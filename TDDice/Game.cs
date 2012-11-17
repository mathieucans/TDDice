using System;
using System.Collections.Generic;

namespace TDDice
{
    public class Game
    {
        private List<IDice> rollableDices;
        private List<IDice> frozenDices;
        private bool played;
        private IScoreManager scoreManager;

        public IEnumerable<IDice> RollableDices
        {
            get { return rollableDices; }
        }

        public IEnumerable<IDice> FrozenDices
        {
            get { return frozenDices; }
        }

        public int Score { get; private set; }

        public Game(IDiceFactory diceFactory, IScoreManager scoreManager)
        {
            rollableDices = new List<IDice>();
            frozenDices = new List<IDice>();

            for (int i = 0; i < 6; i++)
            {
                rollableDices.Add(diceFactory.CreateDice());
            }

            this.scoreManager = scoreManager;
            UpdateScore();
        }

        public void Freeze(IDice dice)
        {
            if (!played)
            {
                throw new Exception();
            }
            rollableDices.Remove(dice);
            frozenDices.Add(dice);
            UpdateScore();
            played = false;
        }

        public void Play()
        {
            foreach (var dice in rollableDices)
            {
                dice.Roll();
            }
            played = true;
        }

        private void UpdateScore()
        {
            Score = scoreManager.Calculate(frozenDices);
        }
    }
}
