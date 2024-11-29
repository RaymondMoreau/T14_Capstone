import express from 'express';
import mysql from 'mysql2';

const app = express();
const port = 5000;

// MySQL connection details
const db = mysql.createConnection({
    host: 'sql5.freesqldatabase.com',
    user: 'sql5747464',
    password: 'GVZmPQHaP2',
    database: 'sql5747464',
    port: 3306
});

// Connect to MySQL
db.connect((err) => {
    if (err) {
        console.error('Database connection failed:', err.stack);
        return;
    }
    console.log('Connected to MySQL database');
});

// Endpoint to fetch one question for each difficulty
app.get('/questions-by-difficulty', (req, res) => {
    const query = `
        (SELECT question_text AS question, answer, difficulty FROM Questions WHERE difficulty = 1 ORDER BY RAND() LIMIT 1)
        UNION ALL
        (SELECT question_text AS question, answer, difficulty FROM Questions WHERE difficulty = 2 ORDER BY RAND() LIMIT 1)
        UNION ALL
        (SELECT question_text AS question, answer, difficulty FROM Questions WHERE difficulty = 3 ORDER BY RAND() LIMIT 1)
    `;
    db.query(query, (err, results) => {
        if (err) {
            console.error('Database query error:', err.stack);
            res.status(500).json({ error: 'Database error' });
            return;
        }
        res.json(results);
    });
});

// Start the server
app.listen(port, () => {
    console.log(`Server is running on http://localhost:${port}`);
});