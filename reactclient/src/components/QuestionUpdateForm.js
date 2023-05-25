import React, { useState } from "react";
import Constants from "../utilities/Constants";

export default function QuestionUpdateForm(props) {
  const initialFormData = Object.freeze({
    title: props.question.title,
    content: props.question.content
  });

  const [formData, setFormData] = useState(initialFormData);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const questionToUpdate = {
      questionId: props.question.questionId,
      title: formData.title,
    };

    const url = Constants.API_URL_UPDATE_QUESTION;

    fetch(url, {
      method: "PUT",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(questionToUpdate)
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        alert(error);
      });

      props.onQuestionUpdated(questionToUpdate);
  };

  return (
    <form className="w-100 px-5">
      <h1 className="mt-5">Updating new the question titled "{props.question.title}".</h1>

      <div className="mt-5">
        <label className="h3 form-label">Question Title</label>
        <input
          value={formData.title}
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
        onClick={() => props.onQuestionUpdated(null)}
        className="btn btn-secondary btn-lg w-100 mt-3"
      >
        Cancel
      </button>
    </form>
  );
}
