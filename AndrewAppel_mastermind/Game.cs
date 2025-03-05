using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AndrewAppel_mastermind;

public class Game
{
    public int maxNumberOfTurns = 10;
    const int maxGuessDigit = 6;
    public string[] validDigits = {"1","2","3","4","5","6"};

    
    public int[] HiddenSolution { get; set;  }

    public Game(){
        HiddenSolution = CreateHiddenSolution();
    }

    public int[] CreateHiddenSolution(){
        int[] solution = {0, 0, 0, 0};

        for (int hiddenDigitIndex=0;hiddenDigitIndex < solution.Length; hiddenDigitIndex++){
            solution[hiddenDigitIndex] =  RandomNumberGenerator.GetInt32(1, 7); // fromInclusive, toExclusive;
        }
        return solution;
    }

    public string AskForGuess(int turnNumber){
        string guess;
        Console.WriteLine($"Turn Number:{turnNumber} -  Input a 4 digit number.  each digit must be 1-6");
        guess = Console.ReadLine() ?? String.Empty;
        return guess;
    }

    public bool IsGuessValid(string guess){
        bool guessIsValid = true;

        if (guess.Length != 4) 
        {
            guessIsValid = false;
        }

        int parsedGuess = 0;
        if (int.TryParse(guess, out parsedGuess))
        {
            for (int hiddenDigitIndex=0;hiddenDigitIndex < guess.Length; hiddenDigitIndex++){
                if (!Array.Exists(validDigits, element => element == guess[hiddenDigitIndex].ToString()) ) 
                {
                    guessIsValid = false;
                }
            }
        }
        else
        {
            guessIsValid = false;
        }

        return guessIsValid;
    }

    public string TakeATurn(int turnNumber){
        string result = string.Empty;

        string guess = AskForGuess(turnNumber);

        if (!IsGuessValid(guess))
            {
                result = "Invalid Guess.  Input a 4 digit number.  each digit must be 1-6";
            } else {
                result = CalculateResponse(guess, HiddenSolution);
            }

        return result;
    }

    public string CalculateResponse(string guess, int[] hiddenSolution){

        string result = string.Empty;

        string strHidden = string.Concat(hiddenSolution.Select(x => x.ToString()));    
        
        string rightNumberRightPlace  = string.Empty;
        string rightNumberWrongPlace = string.Empty;

         for (int guessDigitIndex=0;guessDigitIndex < guess.Length; guessDigitIndex++){
                
                for (int hiddenDigitIndex=0; hiddenDigitIndex < strHidden.Length; hiddenDigitIndex++){

                
                    if (guess[guessDigitIndex] == strHidden[hiddenDigitIndex] ) 
                    {
                        if (guessDigitIndex == hiddenDigitIndex){
                            rightNumberRightPlace += "+";
                            break;
                        } else {
                            rightNumberWrongPlace +="-";
                            continue;
                        }
                    } 
                    
                }
            }

        if (rightNumberRightPlace == "++++") {
            result = $"CORRECT: {guess}"; 
        } else {
            result = rightNumberRightPlace + rightNumberWrongPlace;
        }

        if (result == string.Empty) {
            result = "XXXX";
        }
        return result;

    }
}
