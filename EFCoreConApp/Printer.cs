using static System.Console;

namespace EFCoreConApp
{
  public static class Printer
  {
    public static void PrintHeader(string headerMessage = null, string pattern = "-", int noOfTimesPattern = 100)
    {
      int headerMessageLength = 0;
      if (!string.IsNullOrEmpty(headerMessage))
      {
        headerMessageLength = headerMessage.Length;
      }

      for (int i = 1; i < (noOfTimesPattern - headerMessageLength); i++)
      {
        Write($"{pattern}");
        if (((noOfTimesPattern - headerMessageLength) / 2) == i)
        {
          Write($"{headerMessage}");
        }
      }
      WriteLine($"");
    }

    public static void PrintFooter(string pattern = "-", int noOfTimesPattern = 100)
    {
      for (int i = 1; i < noOfTimesPattern; i++)
      {
        Write($"{pattern}");
      }
      WriteLine($"");
    }

    public static void Print(this string message, string headerMessage = null, string pattern = "-", int noOfTimesPattern = 100)
    {
      PrintHeader($"{headerMessage} --- Type string", pattern, noOfTimesPattern);
      WriteLine($"{message}");
      PrintFooter(pattern, noOfTimesPattern);
    }

    //public static void Print<T>(this T message, string headerMessage = null, string pattern = "-", int noOfTimesPattern = 100)
    //{
    //  PrintHeader($"{headerMessage} --- Type T", pattern, noOfTimesPattern);
    //  WriteLine($"{message}");
    //  PrintFooter(pattern, noOfTimesPattern);
    //}

    public static void Print<T>(this IList<T> sourceList, string headerMessage = null, string pattern = "-", int noOfTimesPattern = 100)
    {
      PrintHeader($"{headerMessage} --- Type IList", pattern, noOfTimesPattern);
      PrintCollection(sourceList);
      PrintFooter(pattern, noOfTimesPattern);
    }

    public static void Print<T>(this IEnumerable<T> sourceList, string headerMessage = null, string pattern = "-", int noOfTimesPattern = 100)
    {
      PrintHeader($"{headerMessage} --- Type IEnu", pattern, noOfTimesPattern);
      PrintCollection(sourceList);
      PrintFooter(pattern, noOfTimesPattern);
    }

    public static void Print<T>(this IQueryable<T> sourceList, string headerMessage = null, string pattern = "-", int noOfTimesPattern = 100)
    {
      PrintHeader($"{headerMessage} --- Type IQue", pattern, noOfTimesPattern);
      PrintCollection(sourceList);
      PrintFooter(pattern, noOfTimesPattern);
    }


    private static void PrintCollection<T>(IEnumerable<T> sourceList)
    {
      int ctr = 1;
      foreach (var item in sourceList)
      {
        WriteLine($"{ctr++} -- {item}");
      }
    }
  }
}