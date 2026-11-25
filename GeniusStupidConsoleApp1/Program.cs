// 1. Заводим банк вопросов
// 2. Заводим банк ответов
// 3. Заволим счетчик
// 4. Выводим вопрос
// 5. получаем ответ пользователя
// 6. Если правильно то счетчик ++
// 7. Выводим счетчик

Console.WriteLine("Ответами на все вопросы являются числа, вводите данные правильно!");
Console.Write("Введите свое имя: ");
List<string> usersTestsHistory = new List<string>()
{
    "Имя пользоваетля----Количество баллов----Диагноз"
};
string userName = Console.ReadLine();
string[] questions = new string[]
    {
        "Сколько будет 2 плюс 2, умноженное на 2?",
        "Бревно нжно распилить на 10 частей. Сколько распилов необходимо сделать?",
        "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
        "Укол делают каждые пол часа. Сколько минут нужно на 3 укола?",
        "5 свечей горело, 2 потухли. Сколько свечей осталось?",
        "Часы пробили 6 раз за 5 секунд. За сколько секунд они пробьют 12 раз?",
        "В семье два отца и два сына. Каждый съел по одному яблоку, но всего было съедено 3 яблока. Сколько человек в семье?",
        "Я — двузначное число. Сумма моих цифр равна 9. Если поменять цифры местами, число увеличится на 27. Какое я число?",
        "Во дворе гуляют куры и кролики. Всего у них 10 голов и 28 ног. Сколько во дворе кроликов?",
        "В ящике лежат носки трёх цветов: красные, синие и чёрные. Сколько носков нужно достать в темноте, чтобы гарантированно получить пару одного цвета?"
    };
string[] answers = new string[]
    {
        "6",
        "9",
        "25",
        "60",
        "2",
        "11",
        "3",
        "36",
        "4",
        "4"
    };
while (true)
{
    string diagnos;
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
    PrintDiagnos(correctAnswersPircent, userName);
    diagnos = Diagnosis(correctAnswersPircent, userName);
    Console.Write("Хотите сыграть снова? 'Да'/'Нет': "); 
    string userTestResult = $"{userName}: {correctAnswersCounter} баллов - {diagnos}";
    usersTestsHistory.Add(userTestResult);
    string choice = Console.ReadLine();
    if (choice == "Нет")
    {
        break;
    }
}

for (int i = 0; i < usersTestsHistory.Count; i++)
{
    Console.WriteLine(usersTestsHistory[i]);
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

static void PrintDiagnos(double correctAnswersPircent, string userName)
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

static string Diagnosis(double correctAnswersPircent, string userName)
{
    if (correctAnswersPircent < 20)
    {
        return "ИДИОТ!";
    }
    else if (correctAnswersPircent < 40)
    {
        return "КРИТИН!";
    }
    else if (correctAnswersPircent < 60)
    {
        return "ДУРАК!";
    }
    else if (correctAnswersPircent < 80)
    {
        return "НОРМАЛЬНЫЙ!";
    }
    else if (correctAnswersPircent < 100)
    {
        return "ТАЛАНТ!";
    }
    else
    {
        return "ГЕНИЙ!";
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