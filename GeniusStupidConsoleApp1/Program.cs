// 1. Заводим банк вопросов
// 2. Заводим банк ответов
// 3. Заволим счетчик
// 4. Выводим вопрос
// 5. получаем ответ пользователя
// 6. Если правильно то счетчик ++
// 7. Выводим счетчик

Console.WriteLine("Ответами на все вопросы являются числа, вводите данные правильно!");
Console.Write("Введите свое имя: ");
string userName = Console.ReadLine();
string[] questions = new string[]
    {
        "Сколько будет 2 плюс 2, умноженное на 2?",
        "Бревно нжно распилить на 10 частей. Сколько распилов необходимо сделать?",
        "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
        "Укол делают каждые пол часа. Сколько минут нужно на 3 укола?",
        "5 свечей горело, 2 потухли. Сколько свечей осталось?",
    };
string[] answers = new string[]
    {
        "6",
        "9",
        "25",
        "60",
        "2",
    };
while (true)
{
    int correctAnswersCounter = 0;
    MixArray(questions, answers);

    for (int i = 0; i < questions.Length; i++)
    {
        Console.WriteLine(questions[i]);
        string answer = GetUserAnswer();
        string correctAnswer = answers[i];
        correctAnswersCounter = CheckingTheUserAnswers(answer, correctAnswer, correctAnswersCounter);
    }
    double correctAnswersPircent = correctAnswersCounter * 100 / answers.Length;
    Diagnosis(correctAnswersPircent, userName);
    Console.Write("Хотите сыграть снова? 'Да'/'Нет': ");
    string choice = Console.ReadLine();
    if (choice == "Нет")
    {
        break;
    }
}

static string GetUserAnswer()
{
    while (true)
    {
        Console.WriteLine("Введите ответ - число: ");
        string answer = Console.ReadLine();
        bool flag = true;
        foreach(char item in answer)
        {
            if (item > '9' || item < '0')
            {
                Console.WriteLine("Серьезно?");
                flag = false;
                break;
            }
        }
        if (flag)
        {
            return answer;
        }
    }
}

static int CheckingTheUserAnswers(string answer, string correctAnswer, int correctAnswersCounter)
{
    if (answer == correctAnswer)
    {
        correctAnswersCounter++;
    }
    return correctAnswersCounter;
}

static void Diagnosis(double correctAnswersPircent, string userName)
{
    Console.Write($"{userName} вы ");
    if (correctAnswersPircent < 20)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ИДИОТ!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 40)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("КРИТИН!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 60)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("ДУРАК!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 80)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("НОРМАЛЬНЫЙ!");
        Console.ResetColor();
    }
    else if (correctAnswersPircent < 100)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("ТАЛАНТ!");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ГЕНИЙ!");
        Console.ResetColor();
    }
}

static void MixArray(string[] questions, string[] answers)
{
    for (int i = questions.Length - 1; i > 0; i--)
    {
        int j = Random.Shared.Next(i + 1);
        (questions[i], questions[j]) = (questions[j], questions[i]);
        (answers[i], answers[j]) = (answers[j], answers[i]);
    }
}