import Nav from "./components/header/Nav";
import Projects from "./components/Main/Projects";
import Home from "./components/Main/Home";
import CreateProject from "./components/Main/CreateProject";
import UpdateProject from "./components/Main/UpdateProject";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Router>
      <Nav />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/projects" element={<Projects />} />
        <Route path="/create-project" element={<CreateProject />} />
        <Route path="/update-project" element ={<UpdateProject />} />
      </Routes>
    </Router>
  );
}

export default App;
