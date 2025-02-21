import { Container, Alert } from "react-bootstrap";
import { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Row from "react-bootstrap/Row";
import { useNavigate } from "react-router-dom";

const CreateProject = () => {
  const navigate = useNavigate();
  const [customers, setCustomers] = useState([]);
  const [error, setError] = useState(null);
  const [formData, setFormData] = useState({
    title:"",
    startDate: "",
    endDate: "",
    description: "",
    statusId: 0,
    productId: 0,
    customerId: 0
  });
  const [managers, setManagers] = useState([]);
  const [product, setProduct] = useState([]);
  const [status, setStatus] = useState([]);
  const [validated, setValidated] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();

    const form = event.currentTarget;

    if (form.checkValidity() === false) {
      event.stopPropagation();
    }

    setValidated(true);
    console.log("Submitting Project Data:", JSON.stringify(formData, null, 2));
    
    try {
      const res = await fetch("http://localhost:5018/api/projects", {
        method: "POST",
        headers: {
          "content-Type": "application/json",
        }, 
          body: JSON.stringify({
          Title: formData.title,
          StartDate: formData.startDate,
          EndDate: formData.endDate,
          StatusId: formData.statusId,
          ManagerId: formData.managerId,
          Description: formData.description,  
          ProductId: formData.productId,
          CustomerId: formData.customerId
        }),
      });

      console.log(res)
  
      if (!res.ok) {
        throw new Error(`Error: ${res.status}`);
      }
  
      const responseData = await res.json(); // get response data
      console.log("Project created:", responseData);

      navigate("/projects");
      alert("Project created successfully!");
      
    } catch (error) {
      console.error("Failed to create project:", error);
      alert(`Failed to create project: ${error.message}`);
    }
  };
  
  const FetchStatus = async () => {
    try {
      setError(null);

      const res = await fetch("http://localhost:5018/api/statusTypes");

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`);
      }

      const fetchedResponse = await res.json();
      setStatus(fetchedResponse);

      console.log(fetchedResponse);
    } catch (error) {
      setError(error);
    }
  };

  useEffect(() => {
    FetchStatus();
  }, []);

  const FetchProducts = async () => {
    try {
      setError(null);

      const res = await fetch("http://localhost:5018/api/products");

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`);
      }

      const fetchedResponse = await res.json();
      setProduct(fetchedResponse);

      console.log("products: ", fetchedResponse);
    } catch (error) {
      setError(error);
    }
  };

  useEffect(() => {
    FetchProducts();
  }, []);

  const FetchUsersWithManagerRole = async () => {
    try {
      setError(null);

      const res = await fetch("http://localhost:5018/api/users/managers");

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`);
      }

      setManagers(await res.json());
    } catch (error) {
      setError(error);
    }
  };

  useEffect(() => {
    FetchUsersWithManagerRole();
  }, []);

  const FetchCustomers = async () => {
    try {
      setError(null);

      const res = await fetch("http://localhost:5018/api/customers");

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`);
      }

      setCustomers(await res.json());

      console.log("Customers:", customers);
    } catch (error) {
      setError(error);
    }
  };

  useEffect(() => {
    FetchCustomers();
  }, []);

  return (
    <Container className="bg-light text-dark">
      <Col className="p-4">
        <h4>PROJECT - CREATE NEW</h4>
        {error && (
          <Alert variant="danger" className="text-center">
            <Alert.Heading>Please try again later</Alert.Heading>
            <p>Error: {error.message}</p>
          </Alert>
        )}
        <Row className="mb-3 mt-3">
          <Form noValidate validated={validated} onSubmit={handleSubmit}>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="4" controlId="validationCustom02">
                <Form.Label>Title</Form.Label>
                <Form.Control 
                value={formData.title} 
                onChange={(e) => setFormData({ ...formData, title: e.target.value })}
                type="text" 
                placeholder="Title"
                required
                />
                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustomUsername">
                <Form.Label>Start date</Form.Label>
                <InputGroup hasValidation>
                  <Form.Control
                    value={formData.startDate} 
                    onChange={(e) => setFormData({ ...formData, startDate: e.target.value })}
                    type="date"
                    placeholder="Start date"
                    aria-describedby="inputGroupPrepend"
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
                value={formData.endDate} 
                onChange={(e) => setFormData({ ...formData, endDate: e.target.value })}
                type="date" 
                placeholder="End date" 
                />
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustom04">
                <Form.Label>Status</Form.Label>
                <Form.Select
                 value={formData.statusId}
                 onChange={(e) => setFormData({ ...formData, statusId: e.target.value })}
                  aria-label="Status"
                  required
                >
                  <option value="">Select a status</option>
                  {status.map((s) => (
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
                 value={formData.managerId}
                 onChange={(e) => setFormData({ ...formData, managerId: e.target.value })}
                required>
                  <option value="">Select a project Manager</option>
                  {managers.map((manager) => (
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
                    value={formData.description}
                    onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                    placeholder="description"
                    aria-describedby="inputGroupPrepend"
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
              value={formData.productId}
              onChange={(e) => setFormData({ ...formData, productId: e.target.value })}
              aria-label="Product"
              required
            >
              <option value="">Select a product</option>
              {product.map((p) => (
                <option key={p.id} value={p.id}>{p.name} time: {p.price} US$/hr</option>
              ))}
            </Form.Select>

            <h5>Customer</h5>
            <Form.Select 
             value={formData.customerId}
             onChange={(e) => setFormData({ ...formData, customerId: e.target.value })}
            required
            >
              <option value="">Select a customer</option>
              {customers.map((customer) => (
                <option value={customer.id} key={customer.id}>
                  {customer.name}
                </option>
              ))}
            </Form.Select>
            <Col className="d-flex gap-2 mt-3">
              <Button variant="secondary" className="rounded-pill px-3">
                Cancle
              </Button>
              <Button
                variant="success"
                className="rounded-pill px-3"
                type="submit"
              >
                Save
              </Button>
            </Col>
          </Form>
        </Row>
      </Col>
    </Container>
  );
};

export default CreateProject;
