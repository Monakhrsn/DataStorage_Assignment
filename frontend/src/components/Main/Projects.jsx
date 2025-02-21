import React, { useState, useEffect } from "react";
import { Container, Alert, ListGroup } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashCan } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";


const Projects = () => {
  const [projects, setProjects] = useState([]);
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  const handleDelete = (id) => {
    console.log(`Delete project with ID: ${id}`);
  };

  const handleSelect = (id) => {
    navigate(`/update-project/${id}`);
  }

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
      <ListGroup as="ul">
        {projects.map((project) => (
          <ListGroup.Item 
          as="li" action 
          key={project.id} 
          onClick={() => handleSelect(project.id)} 
          className="d-flex justify-content-between align-items-center"
          style={{ cursor: "pointer" }}
          >
          {project.title}
          <FontAwesomeIcon
              icon={faTrashCan}
              style={{ color: "red", cursor: "pointer" }}
              onClick={(e) => {
                e.stopPropagation(); 
                handleDelete(project.id);
              }}
            />
          </ListGroup.Item>
        ))}
      </ListGroup>
    </Container>
  )
};

export default Projects;