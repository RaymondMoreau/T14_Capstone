import React from 'react';
import './MathGames.css';

const MathGames = () => {
  return (
    <div>
      <section className="first-boxes">
        <div className="box">
          <a href="https://raymondmoreau.github.io/testWebGL/" className="game-link">
            <h3>Save Benjamin Franklin!</h3>
          </a>
        </div>
        <div className="box">
          <a href="https://tahahoussari.github.io/Walk-The-Plank/" className="game-link">
            <h3>Walk the Plank</h3>
          </a>
        </div>
        <div className="box">
          <a href="https://ashton-hagar.github.io/FamiliarFight/" className="game-link">
            <h3>Familiar Fight!</h3>
          </a>
        </div>
        <div className="box">
          <a href="https://tahahoussari.github.io/back-to-the-future/" className="game-link">
            <h3>Back to the Future</h3>
          </a>
        </div>
      </section>
      <section>
        <div className="box">
          <a href="https://harisvohra11.github.io/TheBirdofMathMountain/" className="game-link">
            <h3>The Bird of Math Mountain</h3>
          </a>
        </div>
        <div className="box">
          <a href="https://harisvohra11.github.io/MathIslandRun/" className="game-link">
            <h3>Math Island Run</h3>
          </a>
        </div>
        <div className="box">
          <h3>Future Game</h3>
        </div>
        <div className="box">
          <h3>Future Game</h3>
        </div>
      </section>
      <section>
        <div className="box">
          <h3>Future Game</h3>
        </div>
        <div className="box">
          <h3>Future Game</h3>
        </div>
        <div className="box">
          <h3>Future Game</h3>
        </div>
        <div className="box">
          <h3>Future Game</h3>
        </div>
      </section>
      <footer>
        <p>© 2024 Imagine Quest Learning. All rights reserved.</p>
      </footer>
    </div>
  );
};

export default MathGames;