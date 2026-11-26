// 1. Заводим банк вопросов
// 2. Заводим банк ответов
// 3. Заволим счетчик
// 4. Выводим вопрос
// 5. получаем ответ пользователя
// 6. Если правильно то счетчик ++
// 7. Выводим счетчик
using System.Text;

Console.WriteLine("Ответами на все вопросы являются числа, вводите данные правильно!");
Console.Write("Введите свое имя: ");
string userName = Console.ReadLine();

string filePath = Path.Combine(
    Environment.CurrentDirectory,
    "result.tsv"
);

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
    string diagnosis;
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
    PrintDiagnosis(correctAnswersPircent, userName);
    diagnosis = Diagnosis(correctAnswersPircent, userName);
    SaveResult(userName, correctAnswersCounter, diagnosis);
    Console.Write("Хотите сыграть снова? 'Да'/'Нет': "); 
    string choice = Console.ReadLine();
    if (choice == "Нет")
    {
        break;
    }
}

Console.Write("Хотите просмотреть таблицу результатов? 'Да'/ 'Нет': ");
string chois = Console.ReadLine();
if (chois == "Да")
{
    VievResults(filePath);
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

static void PrintDiagnosis(double correctAnswersPircent, string userName)
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

static void SaveResult (string userName, int points, string diagnosis)
{
    string filePath = Path.Combine(Environment.CurrentDirectory, "result.tsv");

    if (!File.Exists(filePath))
    {
        File.WriteAllText(filePath, 
        "Имя\tКоличество балов\tДиагноз\n", 
        Encoding.UTF8);

    }

    string result = $"{userName}\t{points}\t{diagnosis}\n";
    File.AppendAllText(filePath, result, Encoding.UTF8);
}

static void VievResults(string filePath)
{
    if (!File.Exists(filePath))
    {
        Console.WriteLine("\nИстория результатов пока что пуста");
        return;
    }
    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
    if (lines.Length <= 1)
    {
        Console.WriteLine("\nИстория результатов пока пуста.");
        return;
    }

    Console.WriteLine("========== ИСТОРИЯ РЕЗУЛЬТАТОВ ==========");
    Console.WriteLine(
        "{0,-20} | {1,-20} | {2}",
        "Имя",
        "Баллы",
        "Диагноз"
    );

    Console.WriteLine(new string('-', 65));

    for (int i = 1; i < lines.Length; i++)
    {
        string[] data = lines[i].Split('\t');

        if (data.Length >= 3)
        {
            Console.WriteLine(
                "{0,-20} | {1,-20} | {2}",
                data[0],
                data[1],
                data[2]
            );
        }
    }
    Console.WriteLine(new string('-', 65));
}