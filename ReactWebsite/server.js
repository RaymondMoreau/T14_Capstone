// server.js
const express = require('express');
const mysql = require('mysql');
const cors = require('cors');
const bodyParser = require('body-parser');

const app = express();
app.use(cors());
app.use(bodyParser.json());

const db = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "DBProject",
    database: "game",
});

db.connect((err) => {
  if (err) {
    console.error('Error connecting: ' + err.stack);
    return;
  }
  console.log('Connected as thread ' + db.threadId);
});

app.post('/add-question', (req, res) => {
  const { question, answer } = req.body;
  const query = 'INSERT INTO questions (question, answer) VALUES (?, ?)';

  db.query(query, [question, answer], (err) => {
    if (err) {
      res.status(500).send('Error adding question');
    } else {
      res.status(200).send('Question added successfully');
    }
  });
});

app.get('/random-question', (req, res) => {
    const query = 'SELECT * FROM questions ORDER BY RAND() LIMIT 1';
  
    db.query(query, (err, result) => {
      if (err) {
        res.status(500).send('Error fetching question');
      } else {
        const jsonResult = JSON.stringify(result[0]);
        console.log("Sending JSON to Unity: ", jsonResult); // Log the JSON
        res.status(200).json(result[0]);
      }
    });
  });

const port = process.env.PORT || 5000;
app.listen(port, () => console.log(`Server running on port ${port}`));