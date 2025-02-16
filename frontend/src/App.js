import Nav from "./components/header/Nav";
import Projects from "./components/Projects";
import Home from "./components/Home";
import CreateProject from "./components/CreateProject";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  return (
    <Router>
      <Nav />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/projects" element={<Projects />} />
        <Route path="/create-project" element={<CreateProject />} />
      </Routes>
    </Router>
  );
}

export default App;
