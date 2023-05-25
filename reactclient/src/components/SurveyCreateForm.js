import React, { useState } from "react";
import Constants from "../utilities/Constants";

export default function SurveyCreateForm(props) {
  const [formData, setFormData] = useState();
  const questions = props.questions;

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const surveyToCreate = {
      surveyId: 0,
    };

    const surveyAnswerToCreate = {
      surveyId: 0,
      answer: formData.answer,
    }

    const url = Constants.API_URL_CREATE_SURVEY;

    fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(surveyToCreate, surveyAnswerToCreate),
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
      })
      .catch((error) => {
        alert(error);
      });

    props.onSurveyCreated(surveyToCreate);
  };

  return (
    <form className="w-100 px-5">
      <h1 className="mt-5">Submit New Survey</h1>

      {questions.map((question, index) => {
        return (
          <div className="mt-5">
            <label className="h3 form-label">{question.title}</label>
            <input
              value={FormData.answer}
              name="answer"
              type="text"
              className="form-control"
              onChange={handleChange}
            />
          </div>
        );
      })}

      <button onClick={handleSubmit} className="btn btn-dark btn-lg w-100 mt-5">
        Submit
      </button>
      <button
        onClick={() => props.onSurveyCreated(null)}
        className="btn btn-secondary btn-lg w-100 mt-3"
      >
        Cancel
      </button>
    </form>
  );
}
