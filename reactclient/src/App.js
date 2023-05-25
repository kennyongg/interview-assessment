import React from "react";
import Question from "./pages/Question";
import Survey from "./pages/Survey";
import Navbar from "./Navbar";
import { Route, Routes } from "react-router-dom";

function App() {
  return (
    <>
      <Navbar />
      <div className="container">
        <Routes>
          <Route path="/questions" element={<Question />} />
          <Route path="/survey" element={<Survey />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
