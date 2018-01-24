// Skeleton written by Joe Zachary for CS 3500, January 2017

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Formulas
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public class Formula
    {
        //AM I SUPPOSED TO DO private IEnumerator<string> tokens; HERE?
        private string formulaString;
        private int openParens, closeParens;
        private string previous;

        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using C#-like syntax for double/int literals), 
        /// variable symbols (a letter followed by zero or more letters and/or digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// Examples of a valid parameter to this constructor are:
        ///     "2.5e9 + x5 / 17"
        ///     "(5 * 2) + 8"
        ///     "x*y-2+35/9"
        ///     
        /// Examples of invalid parameters are:
        ///     "_"
        ///     "-5.3"
        ///     "2 5 + 3"
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula)
        {
            this.previous = "first";
            //check if the parameter is null
            if (formula == null)
                throw new FormulaFormatException("The formula cannot be null.");
            
            //saved for Evaluate
            this.formulaString = formula;

            var tokens = GetTokens(formula);
            //var theEnumerator = tokens.GetEnumerator(); Do I need to use this?-------------------------------------------------------------------------------------------------
            //do I need to allow 4.5e+7?

            bool isFirstVariable = true;
            foreach (string token in tokens)
            {
                //split the string into characters for testing 
                char[] letters = token.ToCharArray();

                //double checking size for my sanity
                if (letters.Length == 0)
                {
                    throw new FormulaFormatException("The formula can't be empty.");
                }

                //check if it's an operator or a single-character variable or digit.
                if (letters.Length == 1)
                {
                    if (!((token == "+") || (token == "-") || (token == "(") || (token == ")") || (token == "*") || (token == "/") || (char.IsLetterOrDigit(letters[0]))))
                    {
                        throw new FormulaFormatException(token + " is invalid.");
                    }

                    if (token == "(")
                        openParens++;

                    if (token == ")")
                        closeParens++;
                }

                //check if it's a longer variable like x5 or x64y8
                else if (char.IsLetter(letters[0]))
                {
                    foreach (char letter in letters)
                    {
                        if (!(char.IsLetterOrDigit(letter)))
                        {
                            throw new FormulaFormatException(token + " is invalid. Variables should start with a letter and be followed only by letters or digits.");
                        }
                    }
                }

                //Check if it's a double
                else if (char.IsDigit(letters[0]))
                {
                    if (!(double.TryParse(token, out double unused2)))
                    {
                        throw new FormulaFormatException(token + " is invalid.");
                    }
                }

                if (isFirstVariable == false)
                {
                    //make sure opening parenthesis and operators are followed by the right thing.
                    if ((previous == "(") || (previous == "+") || (previous == "-") || (previous == "*") || (previous == "/"))
                    {
                        if ((token == "*") || (token == "/") || (token == "+") || (token == "-") || (token == ")"))
                            throw new FormulaFormatException("An opening paranthesis must be followed by a number, variable, or opening parenthesis");
                    }

                    //make sure things that follow numbers, variables, or closing parenthesis are the right thing.
                    if (!((previous == "*") || (previous == "/") || (previous == "+") || (previous == "-") || (previous == "(")))
                    {
                        if (!(((token == "+") || (token == "-") || (token == "*") || (token == "/") || (token == ")"))))
                            throw new FormulaFormatException("A number, variable, or closing parenthesis must be followed by an operator or another closing parenthesis");
                    }
                }

                //check first token
                if (isFirstVariable == true)
                {
                    if ((token == ")") || (token == "*" || (token == "+") || (token == "-") || token == "/"))
                        throw new FormulaFormatException("The first token cannot be a close parenthesis or operator.");
                    isFirstVariable = false;
                }

                //hold on to the previous to check validity of next object
                previous = token;
            }

            //check final token in the formula
            if ((previous == "(") || (previous == "*" || (previous == "+") || (previous == "-") || previous == "/"))
                throw new FormulaFormatException("The last token cannot be an opening parenthesis or operator.");

            //check final number of parenthesis
            if (!(openParens == closeParens))
                throw new FormulaFormatException("Number of open and closign parenthesis don't match.");
        }
        /// <summary>
        /// Evaluates this Formula, using the Lookup delegate to determine the values of variables.  (The
        /// delegate takes a variable name as a parameter and returns its value (if it has one) or throws
        /// an UndefinedVariableException (otherwise).  Uses the standard precedence rules when doing the evaluation.
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, its value is returned.  Otherwise, throws a FormulaEvaluationException  
        /// with an explanatory Message.
        /// </summary>
        public double Evaluate(Lookup lookup)
        {
            var tokens = GetTokens(formulaString);
            Stack<double> values = new Stack<double>();
            Stack<String> operators = new Stack<String>();
            openParens = 0;
            closeParens = 0;

            foreach(string theToken in tokens)
            {
                string theToken = token;
                //if t is a variable
                char[] letters = theToken.ToCharArray();
                if (char.IsLetter(letters[0]))
                {
                    theToken = lookup(theToken);
                }

                //if the token is a double
                if (double.TryParse(theToken,out double unused))
                {
                    if ((operators.Count == 0) || (operators.Peek() == "+") || (operators.Peek() == "-") || (operators.Peek() == "(") || operators.Peek() == ")")
                    {
                        values.Push(double.Parse(theToken));
                    }

                    else if (operators.Peek() == "*")
                    {
                        double product = values.Pop() * double.Parse(theToken);
                        operators.Pop();
                        values.Push(product);
                    }

                    else if (operators.Peek() == "/")
                    {
                        if (double.Parse(theToken) == 0.0)
                            throw new FormulaEvaluationException("You cannot divide by zero.");

                        double quotient = values.Pop() / double.Parse(theToken);
                        operators.Pop();
                        values.Push(quotient);
                    }
                }

                //if the token is a *, /, or (, we just push it onto the stack
                if ((theToken == "*") || (theToken == "/"))
                    operators.Push(theToken);
                if (theToken == "(")
                {
                    operators.Push(theToken);
                    openParens++;
                }

                //if the token is a + or -
                if ((theToken == "+") || (theToken == "-"))
                {
                    if (operators.Count == 0)
                        operators.Push(theToken);

                    else if (operators.Peek() == "+")
                    {
                        double second = values.Pop();
                        double first = values.Pop();

                        operators.Pop();
                        values.Push(first + second); 
                    }

                    else if (operators.Peek() == "-")
                    {
                        double second = values.Pop();
                        double first = values.Pop();

                        operators.Pop();
                        values.Push(first - second);
                    }

                    operators.Push(theToken);
                }

                //if the token is a )
                if ((theToken == ")"))
                {
                    if (operators.Peek() == "+")
                    {
                        double second = values.Pop();
                        double first = values.Pop();

                        operators.Pop();
                        values.Push(first + second);
                    }

                    else if (operators.Peek() == "-")
                    {
                        double second = values.Pop();
                        double first = values.Pop();

                        operators.Pop();
                        values.Push(first - second);
                    }

                    operators.Pop();

                    if (operators.Peek() == "*")
                    {
                        double product = values.Pop() * values.Pop();
                        operators.Pop();
                        values.Push(product);
                    }

                    else if (operators.Peek() == "/")
                    {
                        double second = values.Pop();
                        double first = values.Pop();

                        if (second == 0)
                            throw new FormulaEvaluationException("You tried to divide by zero.");

                        operators.Pop();
                        values.Push(first/second);
                    }
                }
            }

            //at the very end
            if (operators.Count == 0)
                return values.Pop();

            else
            {
                double second = values.Pop();
                double first = values.Pop();

                if (operators.Peek() == "+")
                    values.Push(first+second);

                else if (operators.Peek() == "-")
                    values.Push(first-second);

                return values.Pop();
            }
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of a letter followed by
        /// zero or more digits and/or letters, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens.
            // NOTE:  These patterns are designed to be used to create a pattern to split a string into tokens.
            // For example, the opPattern will match any string that contains an operator symbol, such as
            // "abc+def".  If you want to use one of these patterns to match an entire string (e.g., make it so
            // the opPattern will match "+" but not "abc+def", you need to add ^ to the beginning of the pattern
            // and $ to the end (e.g., opPattern would need to be @"^[\+\-*/]$".)
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z][0-9a-zA-Z]*";

            // PLEASE NOTE:  I have added white space to this regex to make it more readable.
            // When the regex is used, it is necessary to include a parameter that says
            // embedded white space should be ignored.  See below for an example of this.
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern.  It contains embedded white space that must be ignored when
            // it is used.  See below for an example of this.  This pattern is useful for 
            // splitting a string into tokens.
            String splittingPattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            // PLEASE NOTE:  Notice the second parameter to Split, which says to ignore embedded white space
            /// in the pattern.
            foreach (String s in Regex.Split(formula, splittingPattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// A Lookup method is one that maps some strings to double values.  Given a string,
    /// such a function can either return a double (meaning that the string maps to the
    /// double) or throw an UndefinedVariableException (meaning that the string is unmapped 
    /// to a value. Exactly how a Lookup method decides which strings map to doubles and which
    /// don't is up to the implementation of the method.
    /// </summary>
    public delegate double Lookup(string var);

    /// <summary>
    /// Used to report that a Lookup delegate is unable to determine the value
    /// of a variable.
    /// </summary>
    [Serializable]
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Constructs an UndefinedVariableException containing whose message is the
        /// undefined variable.
        /// </summary>
        /// <param name="variable"></param>
        public UndefinedVariableException(String variable)
            : base(variable)
        {
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the parameter to the Formula constructor.
    /// </summary>
    [Serializable]
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    [Serializable]
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message) : base(message)
        {
        }
    }
}
