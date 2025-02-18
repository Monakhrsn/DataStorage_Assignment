import React, { useState, useEffect } from "react";
import { Container, Alert } from "react-bootstrap";


const Projects = () => {
  const [projects, setProjects] = useState([]);
  const [error, setError] = useState(null);

  const FetchProjects = async () => {
    try {
      setError(null);

      const res = await fetch(
        "http://localhost:5018/api/projects"
      )

      if (!res.ok) {
        throw new Error(`An Error occured! Status: ${res.status}`)
      }

      const fetchedResponse = await res.json();
      setProjects(fetchedResponse);

    } catch (error) {
      setError(error);
    }
  }

  useEffect(() => {
    FetchProjects();
  }, []);

  return (
    <Container className="mt-4">
      <h2>Projects</h2>

      {error && (
        <Alert variant="danger" className="text-center">
          <Alert.Heading>Please try again later</Alert.Heading>
          <p>Error: {error.message}</p>
        </Alert>
      )}
      <ul>
        {projects.map((project) => (
          <li key={project.id}>
          {project.title}
        </li>
        ))}
      </ul>
    </Container>
  )
};

export default Projects;