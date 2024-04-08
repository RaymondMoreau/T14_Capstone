// QuestionForm.js
import React, { useState } from 'react';
import axios from 'axios';

const QuestionForm = () => {
  const [question, setQuestion] = useState('');
  const [answer, setAnswer] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await axios.post('http://localhost:5000/add-question', { question, answer });
      setQuestion('');
      setAnswer('');
      alert('Question added successfully!');
    } catch (error) {
      console.error(error);
      alert('Failed to add the question.');
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-md-center">
        <div className="col-md-6">
          <h1 className="mb-3">Add a Question</h1>
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="questionInput" className="form-label">Question</label>
              <input
                type="text"
                className="form-control"
                id="questionInput"
                value={question}
                onChange={(e) => setQuestion(e.target.value)}
                placeholder="Enter question"
                required
              />
            </div>
            <div className="mb-3">
              <label htmlFor="answerInput" className="form-label">Answer</label>
              <input
                type="text"
                className="form-control"
                id="answerInput"
                value={answer}
                onChange={(e) => setAnswer(e.target.value)}
                placeholder="Enter answer"
                required
              />
            </div>
            <button type="submit" className="btn btn-primary">Submit</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default QuestionForm;