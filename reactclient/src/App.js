import React, { useState } from "react";
import Constants from "./utilities/Constants";
import QuestionCreateForm from "./components/QuestionCreateForm";
import QuestionUpdateForm from "./components/QuestionUpdateForm";

export default function App() {
  const [questions, setQuestions] = useState([]);
  const [showingCreateNewQuestionForm, setShowingCreateNewQuestionForm] =
    useState(false);
  const [questionCurrentlyBeingUpdated, setQuestionCurrentlyBeingUpdated] = useState(null);

  function getQuestions() {
    const url = Constants.API_URL_GET_ALL_QUESTIONS;

    fetch(url, {
      method: "GET",
    })
      .then(response => response.json())
      .then((questionsFromServer) => {
        setQuestions(questionsFromServer);
      })
      .catch((error) => {
        alert(error);
      });
  }

  function deleteQuestion(questionId) {
    const url = `${Constants.API_URL_DELETE_QUESTION_BY_ID}/${questionId}`;

    fetch(url, {
      method: "DELETE",
    })
      .then(response => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
        onQuestionDeleted(questionId);
      })
      .catch((error) => {
        alert(error);
      });
  }

  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {(showingCreateNewQuestionForm === false && questionCurrentlyBeingUpdated === null) && (
            <div>
              <h1>Survey Questions</h1>
              <div className="mt-5">
                <button
                  onClick={getQuestions}
                  className="btn btn-dark btn-lg w-100"
                >
                  Get Questions From Server
                </button>
                <button
                  onClick={() => {setShowingCreateNewQuestionForm(true)}}
                  className="btn btn-secondary btn-lg w-100 mt-4"
                >
                  Create New Question
                </button>
              </div>
            </div>
          )}

          {(questions.length > 0 &&
            showingCreateNewQuestionForm === false && questionCurrentlyBeingUpdated === null) &&
            renderQuestionsTable()}

          {showingCreateNewQuestionForm && (
            <QuestionCreateForm onQuestionCreated={onQuestionCreated} />
          )}

          {questionCurrentlyBeingUpdated !== null && <QuestionUpdateForm question={questionCurrentlyBeingUpdated} onQuestionUpdated={onQuestionUpdated} />}
        </div>
      </div>
    </div>
  );

  function renderQuestionsTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">QuestionId</th>
              <th scope="col">Question Title</th>
              <th scope="col">Actions</th>
            </tr>
          </thead>
          <tbody>
            {questions.map((question) => (
              <tr key={question.questionId}>
                <th scope="row">{question.questionId}</th>
                <td>{question.title}</td>
                <td>
                  <button onClick={() => setQuestionCurrentlyBeingUpdated(question)} className="btn btn-dark btn-lg mx-3 my-3">
                    Update
                  </button>
                  <button onClick={() => { if(window.confirm(`Are you sure you want to delete the question titled "${question.title}"?`)) deleteQuestion(question.questionId) }} className="btn btn-secondary btn-lg">Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }

  function onQuestionCreated(createdQuestion) {
    setShowingCreateNewQuestionForm(false);

    if (createdQuestion === null) {
      return;
    }

    alert(
      `Question successfully created. After clicking OK, your new question titled "${createdQuestion.title}" will show up in the table below.`
    );

    getQuestions();
  }

  function onQuestionUpdated(updatedQuestion) {
    setQuestionCurrentlyBeingUpdated(null);

    if (updatedQuestion === null) {
      return;
    }

    let questionsCopy = [...questions];

    const index = questionsCopy.findIndex((questionsCopyQuestion, currentIndex) => {
      if (questionsCopyQuestion.questionId === updatedQuestion.questionId) {
        return true;
      }
    });

    if (index !== -1) {
      questionsCopy[index] = updatedQuestion;
    }

    setQuestions(questionsCopy);

    alert(`Question successfully updated. After clicking OK, look for the question with the title "${updatedQuestion.title}" in the table below to see the updates.`);
  }

  function onQuestionDeleted(deletedQuestionQuestionId) {
    let questionsCopy = [...questions];

    const index = questionsCopy.findIndex((questionsCopyQuestion, currentIndex) => {
      if (questionsCopyQuestion.questionId === deletedQuestionQuestionId) {
        return true;
      }
    });

    if (index !== -1) {
      questionsCopy.splice(index, 1);
    }

    setQuestions(questionsCopy);

    alert(`Question successfully deleted. After clicking OK, look at the table to see your question disappear.`);
  }
}
