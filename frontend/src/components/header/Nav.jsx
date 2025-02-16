import {
  Navbar,
  Row,
  Col,
  Container,
  Nav as BootstrapNav,
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

        <div className="d-flex justify-content-between gap-3">
          <BootstrapNav.Link as={Link} to="/projects">Projects</BootstrapNav.Link>
          <BootstrapNav.Link as={Link} to="/create-project">Create Project</BootstrapNav.Link>
        </div>
      </Container>
    </Navbar>
  );
};

export default Nav;
