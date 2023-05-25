import React, { useState } from "react";
import Constants from "../utilities/Constants";

export default function QuestionCreateForm(props) {

  const [formData, setFormData] = useState();

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const questionToCreate = {
      questionId: 0,
      title: formData.title,
    };

    const url = Constants.API_URL_CREATE_QUESTION;

    fetch(url, {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(questionToCreate)
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        alert(error);
      });

      props.onQuestionCreated(questionToCreate);
  };

  return (
    <form className="w-100 px-5">
      <h1 className="mt-5">Create new question</h1>

      <div className="mt-5">
        <label className="h3 form-label">Question Title</label>
        <input
          value={FormData.title}
          name="title"
          type="text"
          className="form-control"
          onChange={handleChange}
        />
      </div>

      <button
        onClick={handleSubmit}
        className="btn btn-dark btn-lg w-100 mt-5"
      >
        Submit
      </button>
      <button
        onClick={() => props.onQuestionCreated(null)}
        className="btn btn-secondary btn-lg w-100 mt-3"
      >
        Cancel
      </button>
    </form>
  );
}
