import {
  Container,
  Alert,
  Form,
  Row,
  Col,
  Button,
  InputGroup,
} from "react-bootstrap";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

const UpdateProject = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [isEditing, setIsEditing] = useState(false);
  const [validated, setValidated] = useState(false);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [customerOptions, setCustomerOptions] = useState([]);
  const [formData, setFormData] = useState({
    title: "",
    startDate: "",
    endDate: null,
    description: "",
    statusId: 0,
    productId: 0,
    customerId: 0,
  });
  const [managerOptions, setManagerOptions] = useState([]);
  const [productOptions, setProductOptions] = useState([]);
  const [statusOptions, setStatusOptions] = useState([]);

  const onEditMode = (event) => {
    event.preventDefault()
    setIsEditing(true)
  }

  const onCancel = () => {
    navigate("/projects");
  }
  // Get projectDetails
  const fetchProject = async () => {
    try {
      setLoading(true);
      const res = await fetch(`http://localhost:5018/api/projects/${id}`);

      if (!res.ok) {
        if (res.status === 404) throw new Error("Project not found");
      }

      const fetchedProject = await res.json();
      console.log("Fetched Project Data:", fetchedProject);

      setFormData({
        title: fetchedProject.title,
        startDate: fetchProject.startDate ? fetchedProject.startDate.split("T")[0] : "",
        endDate: fetchedProject.endDate ? fetchedProject.endDate.split("T")[0] : "",
        description: fetchedProject.description,
        statusId: fetchedProject.statusId,
        productId: fetchedProject.productId,
        customerId: fetchedProject.customerId,
        managerId: fetchedProject.managerId,
      });
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    fetchProject();
  }, [id]);

  // Fetch options
  useEffect(() => {
    const fetchOptions = async () => {
      try {
        // suggested by chatGpt
        const [statusRes, productRes, customerRes, managerRes] =
          await Promise.all([
            fetch("http://localhost:5018/api/statusTypes"),
            fetch("http://localhost:5018/api/products"),
            fetch("http://localhost:5018/api/customers"),
            fetch("http://localhost:5018/api/users/managers"),
          ]);

        if (
          !statusRes.ok ||
          !productRes.ok ||
          !customerRes.ok ||
          !managerRes.ok
        )
          throw new Error("Error fetching dropdown options");

        setStatusOptions(await statusRes.json());
        setProductOptions(await productRes.json());
        setCustomerOptions(await customerRes.json());
        setManagerOptions(await managerRes.json());
      } catch (error) {
        console.error("Error fetching dropdown options", error);
      }
    };

    fetchOptions();
  }, []);

  const handleUpdateSubmit = async (event) => {
    event.preventDefault();
    setValidated(false);

    try {
      const res = await fetch(`http://localhost:5018/api/projects/${id}`, {
        method: "PUT",
        headers: {
          "content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      if (!res.ok) {
        throw new Error(`Error updating project: ${res.status}`);
      }
      console.log(res);

      const fetchedUpdatedResponse = await res.json();
      console.log("Updated Project Data:", fetchedUpdatedResponse);

      setFormData({
        title: fetchedUpdatedResponse.title,
        startDate: fetchedUpdatedResponse.startDate ? fetchedUpdatedResponse.startDate.split("T")[0] : "",
        endDate: fetchedUpdatedResponse.endDate ? fetchedUpdatedResponse.endDate.split("T")[0] : "",
        description: fetchedUpdatedResponse.description,
        statusId: fetchedUpdatedResponse.statusId,
        productId: fetchedUpdatedResponse.productId,
        customerId: fetchedUpdatedResponse.customerId,
        managerId: fetchedUpdatedResponse.managerId,
      });
      
      navigate("/projects");
    } catch (error) {
      console.error(error.message);
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  if (loading) return <p>Loading Project details.. .</p>;

  return (
    <Container className="bg-light text-dark">
      <Col className="p-4">
        <h4>PROJECT NR.{id} - EDIT</h4>
        {error && (
          <Alert variant="danger" className="text-center">
            <Alert.Heading>Please try again later: {error}</Alert.Heading>
            <p>Error: {error.message}</p>
          </Alert>
        )}
        <Row className="mb-3 mt-3">
          <Form noValidate validated={validated} onSubmit={handleUpdateSubmit}>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="4" controlId="validationCustom02">
                <Form.Label>Title</Form.Label>
                <Form.Control
                  value={formData.title}
                  onChange={handleChange}
                  type="text"
                  name="title"
                  disabled={!isEditing}
                  required
                />
                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustomUsername">
                <Form.Label>Start date</Form.Label>
                <InputGroup hasValidation>
                  <Form.Control
                    name="startDate"
                    value={formData.startDate}
                    onChange={handleChange}
                    type="date"
                    disabled={!isEditing}
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please choose a valid start date.
                  </Form.Control.Feedback>
                </InputGroup>
              </Form.Group>
              <Form.Group as={Col} md="6" controlId="validationCustom03">
                <Form.Label>End date</Form.Label>
                <Form.Control
                  name="endDate"
                  value={formData.endDate}
                  onChange={handleChange}
                  type="date"
                  disabled={!isEditing}
                />
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustom04">
                <Form.Label>Status</Form.Label>
                <Form.Select
                  name="statusId"
                  value={formData.statusId}
                  onChange={handleChange}
                  disabled={!isEditing}
                  required
                >
                  <option value="">Select a status</option>
                  {statusOptions.map((s) => (
                    <option key={s.id} value={s.id}>
                      {s.name}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Row>
            <h5>Additional information</h5>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="4" controlId="validationCustom02">
                <Form.Label>Project manager</Form.Label>
                <Form.Select
                  type="text"
                  name="managerId"
                  value={formData.managerId}
                  onChange={handleChange}
                  disabled={!isEditing}
                  required
                >
                  <option value="">Select a project Manager</option>
                  {managerOptions.map((manager) => (
                    <option value={manager.id} key={manager.id}>
                      {manager.firstName} {manager.lastName}
                    </option>
                  ))}
                </Form.Select>
                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustomUsername">
                <Form.Label>Description</Form.Label>
                <InputGroup hasValidation>
                  <Form.Control
                    type="text"
                    name="description"
                    value={formData.description}
                    onChange={handleChange}
                    disabled={!isEditing}
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please write a description.
                  </Form.Control.Feedback>
                </InputGroup>
              </Form.Group>
            </Row>
            <h5>Selected Products</h5>
            <Form.Select
              name="productId"
              value={formData.productId}
              onChange={handleChange}
              disabled={!isEditing}
              required
            >
              <option value="">Select a product</option>
              {productOptions.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.name}
                </option>
              ))}
            </Form.Select>

            <h5>Customer</h5>
            <Form.Select
              name="customerId"
              value={formData.customerId}
              onChange={handleChange}
              disabled={!isEditing}
              required
            >
              <option value="">Select a customer</option>
              {customerOptions.map((customer) => (
                <option value={customer.id} key={customer.id}>
                  {customer.name}
                </option>
              ))}
            </Form.Select>
            <Col className="d-flex gap-2 mt-3">
              {!isEditing ? (
                <Button
                  variant="outline-warning"
                  type="button"
                  onClick={onEditMode}
                  className="rounded-pill px-3"
                >
                  Edit
                </Button>
                
              ) : (
                <Button
                  variant="success"
                  type="submit"
                  className="rounded-pill px-3"
                >
                  Update
                </Button>
              )}
              <Button variant="secondary" 
                className="rounded-pill px-3"
                onClick={onCancel}>
                Cancle
              </Button>
            </Col>
          </Form>
        </Row>
      </Col>
    </Container>
  );
};

export default UpdateProject;
