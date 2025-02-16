import React from "react";
import { Container } from "react-bootstrap";


const Projects = () => {
  const projects = [
    { id: 1, name: "Project A" },
    { id: 2, name: "Project B" },
    { id: 3, name: "Project C" }
  ]

  return (
    <Container mt-4>
      <h2>Projects</h2>
      <ul>
        {projects.map((project) => (
          <li key={project.id}>
          {project.name}
        </li>
        ))}
      </ul>
    </Container>
  )
};

export default Projects;