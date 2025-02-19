import { Container, Alert } from "react-bootstrap";
import { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Row from "react-bootstrap/Row";

const CreateProject = () => {
  const [validated, setValidated] = useState(false);
  const [status, setStatus] = useState(null);
  const [product, setProduct] = useState(null);
  const [error, setError] = useState(null);
  const [managers, setManagers] = useState([]);

  const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
      event.preventDefault();
      event.stopPropagation();
    }

    setValidated(true);
  };

  const FetchStatus = async () => {
    try { 
      setError(null);

      const res = await fetch(
        "http://localhost:5018/api/statusTypes"
      )

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`)
      }

      const fetchedResponse = await res.json();
      setStatus(fetchedResponse);

      console.log(fetchedResponse)
    } catch (error) {
      setError(error);
    }
  }

  useEffect(() => {
    FetchStatus();
  }, []);


  const FetchProducts = async () => {
    try { 
      setError(null);

      const res = await fetch(
        "http://localhost:5018/api/products"
      )

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`)
      }

      const fetchedResponse = await res.json();
      setProduct(fetchedResponse);

      console.log("products: ", fetchedResponse)
    } catch (error) {
      setError(error);
    }
  }

  useEffect(() => {
    FetchProducts();
  }, []);

  const FetchUsersWithManagerRole = async () => {
    try { 
      setError(null);

      const res = await fetch(
        "http://localhost:5018/api/users/managers"
      )

      if (!res.ok) {
        throw new Error(`An error occured! Status: ${res.status}`)
      }

      setManagers(await res.json());
    } catch (error) {
      setError(error);
    }
  }

  useEffect(() => {
    FetchUsersWithManagerRole();
  }, []);


  return (
    <Container className="bg-light text-dark">
      <Col className="p-4">
        <h4>PROJECT  - CREATE NEW</h4>
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
                <Form.Control required type="text" placeholder="Title" />
                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustomUsername">
                <Form.Label>Start date</Form.Label>
                <InputGroup hasValidation>
                  <Form.Control
                    type="text"
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
                <Form.Control type="text" placeholder="End date" />
              </Form.Group>
              <Form.Group as={Col} md="4" controlId="validationCustom04">
                <Form.Label>Status</Form.Label>
                <Form.Select onSelect={(value) => setStatus(value)} aria-label="Status" required>
                  <option value="1">Not starteddd</option>
                  <option value="2">Ongoingg</option>
                  <option value="3">Finisheddd</option>
                </Form.Select>
              </Form.Group>
            </Row>
            <h5>Additional information</h5>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="4" controlId="validationCustom01">
                <Form.Label>Hours</Form.Label>
                <Form.Control required type="text" placeholder="hours" />
                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
              </Form.Group>


              <Form.Group as={Col} md="4" controlId="validationCustom02">
                <Form.Label>Project manager</Form.Label>
                <Form.Select
                  required
                >
                <option value="">Select a project Manager</option>
                {managers.map((manager) => (
                  <option key={manager.id} value={manager.id}>
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
                    placeholder="description"
                    aria-describedby="inputGroupPrepend"
                    required
                  />
                  <Form.Control.Feedback type="invalid">
                    Please write a description.
                  </Form.Control.Feedback>
                </InputGroup>
              </Form.Group>
              <Form.Group as={Col} md="6" controlId="validationCustom03">
                <Form.Label>Price</Form.Label>
                <Form.Control type="text" placeholder="Price" required />
                <Form.Control.Feedback type="invalid">
                  Please provide a valid price.
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <h5>Selected Products</h5>
            <Form.Select onSelect={(value) => setProduct(value)} aria-label="Product" required>
                <option value="1">Educationnn</option>
                <option value="2">Consultant</option>
            </Form.Select>
            <h5>Customer</h5>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="6" controlId="validationCustom03">
                <Form.Label>Customer</Form.Label>
               <span> Info comes according to the customer id</span>
              </Form.Group>
            </Row>
              <Col className="d-flex gap-2">
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
