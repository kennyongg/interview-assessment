import React, { useState } from "react";
import Constants from "../utilities/Constants";
import SurveyCreateForm from "../components/SurveyCreateForm";
import { useParams } from "react-router-dom";

export default function Survey() {
  const [questions, setQuestions] = useState([]);
  const [surveys, setSurveys] = useState([]);
  const [showingCreateNewSurveyForm, setShowingCreateNewSurveyForm] =
    useState(false);

  function getQuestions() {
    const url = Constants.API_URL_GET_ALL_QUESTIONS;

    fetch(url, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((questionsFromServer) => {
        setQuestions(questionsFromServer);
      })
      .catch((error) => {
        alert(error);
      });
  }

  function getSurveys() {
    const url = Constants.API_URL_GET_ALL_SURVEYS;

    fetch(url, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((surveysFromServer) => {
        setSurveys(surveysFromServer);
      })
      .catch((error) => {
        alert(error);
      });
  }

  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {showingCreateNewSurveyForm ===
            false && (
              <div>
                <h1>Survey Questions</h1>
                <div className="mt-5">
                  <button
                    onClick={getSurveys}
                    className="btn btn-dark btn-lg w-100"
                  >
                    Get Surveys From Server
                  </button>
                  <button
                    onClick={() => {
                      setShowingCreateNewSurveyForm(true); getQuestions()
                    }}
                    className="btn btn-secondary btn-lg w-100 mt-4"
                  >
                    Submit New Survey
                  </button>
                </div>
              </div>
            )}

          {surveys.length > 0 &&
            showingCreateNewSurveyForm === false &&
            renderSurveysTable()}

          {showingCreateNewSurveyForm && (
            <SurveyCreateForm
              onSurveyCreated={onSurveyCreated}
              questions={questions}
            />
          )}
        </div>
      </div>
    </div>
  );

  function renderSurveysTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">SurveyId</th>
              <th scope="col">Submitted At</th>
              <th scope="col">Actions</th>
            </tr>
          </thead>
          <tbody>
            {surveys.map((survey) => (
              <tr key={survey.surveyId}>
                <th scope="row">{survey.surveyId}</th>
                <td>{survey.submitTime}</td>
                <td>
                  <button
                    onClick={() => {
                      
                    }}
                    className="btn btn-secondary btn-lg"
                  >
                    Show
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }

  function onSurveyCreated(createdSurvey) {
    setShowingCreateNewSurveyForm(false);

    if (createdSurvey === null) {
      return;
    }

    alert(
      `Survey successfully created. After clicking OK, your new survey titled "${createdSurvey.title}" will show up in the table below.`
    );

    getSurveys();
  }
}
