import { Container } from "react-bootstrap";
import { useState } from "react";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Row from "react-bootstrap/Row";
import Dropdown from "react-bootstrap/Dropdown";

const UpdateProject = () => {
  const [validated, setValidated] = useState(false);
  const [status, setStatus] = useState("Choose the status of the project");
  const [product, setProduct] = useState("Choose the type of the product");

  const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
      event.preventDefault();
      event.stopPropagation();
    }

    setValidated(true);
  };
  return (
    <Container className="bg-light text-dark">
      <Col className="p-4">
        <h4>PROJECT 218 - EDIT</h4>
        <Row className="mb-3 mt-3">
          <Form noValidate validated={validated} onSubmit={handleSubmit}>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="4" controlId="validationCustom01">
                <Form.Label>Project number</Form.Label>
                <Form.Control required type="text" placeholder="First name" />
                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
              </Form.Group>
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
                <Dropdown
                  onSelect={(eventKey) => setStatus(eventKey)}
                  aria-label="Status"
                  required
                >
                  <Dropdown.Toggle variant="light" className="bg-white border">
                    {status}
                  </Dropdown.Toggle>
                  <Dropdown.Menu>
                    <Dropdown.Item eventKey="Not starteddd">
                      Not starteddd
                    </Dropdown.Item>
                    <Dropdown.Item eventKey="Ongoingg">Ongoingg</Dropdown.Item>
                    <Dropdown.Item eventKey="Finisheddd">
                      Finisheddd
                    </Dropdown.Item>
                  </Dropdown.Menu>
                </Dropdown>
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
                <Form.Control
                  required
                  type="text"
                  placeholder="project manager"
                />
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
            <Dropdown
              onSelect={(eventKey) => setProduct(eventKey)}
              aria-label="Product"
              required
            >
              <Dropdown.Toggle variant="light" className="bg-white border">
                {product}
              </Dropdown.Toggle>
              <Dropdown.Menu>
                <Dropdown.Item eventKey="Educationnn">
                  Educationnn
                </Dropdown.Item>
                <Dropdown.Item eventKey="Consultant">Consultant</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
            <h5>Customer</h5>
            <Row className="mb-3 border-top">
              <Form.Group as={Col} md="6" controlId="validationCustom03">
                <Form.Label>Customer Name</Form.Label>
                <Form.Control type="text" placeholder="John Doe" required />
                <Form.Control.Feedback type="invalid">
                  Please provide a valid customer name.
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Col className="d-flex justify-content-between">
              <Button variant="outline-warning" className="rounded-pill px-3">
                Edit
              </Button>
              <Stack direction="horizental" className="gap-2">
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
              </Stack>
            </Col>
          </Form>
        </Row>
      </Col>
    </Container>
  );
};

export default CreateProject;
