import {
  Navbar,
  Container,
  Nav as BootstrapNav,
  Button,
} from "react-bootstrap";
import { Link } from "react-router-dom";
import { useState } from "react";

const Nav = () => {
  const [expanded, setExpanded] = useState(false);
  return (
    <Navbar
      expanded={expanded}
      expand="xl"
      onToggle={() => setExpanded(!expanded)}
    >
      <Container>
        <Navbar.Brand as={Link} to="/">
          <span>Hans Mattin AB</span>
        </Navbar.Brand>
        <div className="d-flex justify-content-between gap-2">
          <Button variant="secondary" className="rounded-pill px-3">
            <BootstrapNav.Link as={Link} to="/create-project">
              Create Project
            </BootstrapNav.Link>
          </Button>
          <Button variant="warning" className="rounded-pill px-3">
            <BootstrapNav.Link as={Link} to="/projects">
              Show Projects
            </BootstrapNav.Link>
          </Button>
        </div>
      </Container>
    </Navbar>
  );
};

export default Nav;
