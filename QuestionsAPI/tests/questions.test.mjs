import mysql from 'mysql2';
import { expect } from 'chai';

// MySQL connection details
const db = mysql.createConnection({
    host: '127.0.0.1',
    user: 'root',             // Replace with your MySQL username
    password: 'root',          // Replace with your MySQL password
    database: 'mathquestionsdb'
});

// Utility function to evaluate a math expression safely
function evaluateExpression(expression) {
    try {
        return new Function(`return ${expression}`)();
    } catch (error) {
        throw new Error(`Invalid expression: ${expression}`);
    }
}

// Utility functions for validation and business logic
function validateQuestionFormat(question) {
    const regex = /^[0-9+\-*/().\s]+$/; // Simple regex for allowed characters
    return regex.test(question);
}

function isValidAnswer(answer) {
    return typeof answer === 'number' && !isNaN(answer);
}

function determineDifficulty(question) {
    if (question.includes('*') || question.includes('/')) return 'Hard';
    if (question.includes('+') || question.includes('-')) return 'Easy';
    return 'Unknown';
}

// Combined Tests
describe('Questions API, Validation, Business Logic, and Database Tests', function() {

    // Database Connection Setup
    before((done) => {
        db.connect((err) => {
            if (err) {
                done(err);
            } else {
                console.log('Connected to MySQL database for testing');
                done();
            }
        });
    });

    // Data Validation Tests
    describe('Data Validation Tests', function() {
        it('should validate correct question formats', () => {
            expect(validateQuestionFormat("2 + 2")).to.be.true;
            expect(validateQuestionFormat("5 * (3 + 2)")).to.be.true;
            expect(validateQuestionFormat("10 / 5 + 3")).to.be.true;
            expect(validateQuestionFormat("(8 - 2) * 4")).to.be.true;
        });

        it('should reject invalid question formats', () => {
            expect(validateQuestionFormat("abc")).to.be.false;
            expect(validateQuestionFormat("2 + 2 = 4")).to.be.false; // "= 4" should not be allowed
            expect(validateQuestionFormat("5 + * 3")).to.be.false;
            expect(validateQuestionFormat("2 / / 4")).to.be.false;
        });

        it('should validate correct answer formats', () => {
            expect(isValidAnswer(4)).to.be.true;
            expect(isValidAnswer(4.5)).to.be.true;
            expect(isValidAnswer(-10)).to.be.true;
            expect(isValidAnswer(0)).to.be.true;
        });

        it('should reject incorrect answer formats', () => {
            expect(isValidAnswer("not a number")).to.be.false;
            expect(isValidAnswer(null)).to.be.false;
            expect(isValidAnswer(NaN)).to.be.false;
            expect(isValidAnswer(undefined)).to.be.false;
        });
    });

    // Business Logic Tests
    describe('Business Logic Tests', function() {
        it('should categorize question as Easy for addition/subtraction', () => {
            expect(determineDifficulty("2 + 2")).to.equal("Easy");
            expect(determineDifficulty("5 - 3")).to.equal("Easy");
            expect(determineDifficulty("10 + 20 - 5")).to.equal("Easy");
        });

        it('should categorize question as Hard for multiplication/division', () => {
            expect(determineDifficulty("3 * 4")).to.equal("Hard");
            expect(determineDifficulty("10 / 2")).to.equal("Hard");
            expect(determineDifficulty("8 * 5 / 2")).to.equal("Hard");
        });

        it('should return Unknown for unsupported operations', () => {
            expect(determineDifficulty("2 ^ 3")).to.equal("Unknown");
            expect(determineDifficulty("sqrt(4)")).to.equal("Unknown");
            expect(determineDifficulty("log(10)")).to.equal("Unknown");
        });
    });

    // Database Question and Answer Test
    describe('Database Question and Answer Test', function() {
        it('should correctly evaluate each question and match it with the answer', (done) => {
            const query = 'SELECT question_text, answer FROM questions';
            db.query(query, (err, results) => {
                if (err) {
                    done(err);
                    return;
                }

                results.forEach((row, index) => {
                    const { question_text, answer } = row;
                    const calculatedAnswer = evaluateExpression(question_text);
                    const expectedAnswer = parseFloat(answer);

                    try {
                        expect(calculatedAnswer).to.be.closeTo(expectedAnswer, 0.01);
                    } catch (error) {
                        console.error(`Test failed for question ${index + 1}: "${question_text}" - Expected: ${expectedAnswer}, Got: ${calculatedAnswer}`);
                        expect.fail(`Test failed for question ${index + 1}: "${question_text}" - Expected: ${expectedAnswer}, Got: ${calculatedAnswer}`);
                    }
                });

                done();
            });
        });

        it('should handle empty results from the database without errors', (done) => {
            const query = 'SELECT question_text, answer FROM questions WHERE 1=0'; // Query that returns no results
            db.query(query, (err, results) => {
                if (err) {
                    done(err);
                    return;
                }

                expect(results).to.be.an('array').that.is.empty;
                done();
            });
        });
    });

    // Database Disconnection
    after((done) => {
        db.end((err) => {
            if (err) {
                done(err);
            } else {
                console.log('Disconnected from MySQL database after testing');
                done();
            }
        });
    });
});
