using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;




namespace CSharp18
{

    class Item
    {
        public string name;
        public int level;
        public int price;

        public Item(string name, int level, int price)
        {
            this.name = name;
            this.level = level;
            this.price = price;
        }
    }
    internal class Program
    {

        //select
        //어떠한 데이터를 사용할 것인지 기준을 잡는다.
        static void Main(string[] args)
        {
            if (false)
            {

                Item[] items = {
                new Item("단검", 10,4000),
                new Item("지팡이", 10,3500),
                new Item("사냥꾼의활", 10,28000),
                new Item("그룬힐", 10,35000),
                new Item("청운검", 10,100000),

            };

                //where
                //데이터 필터, 원하는 조건에 맞는 데이터만 수집한다.


                //Group by
                // = 데이터를 특정 기준을 잡아 분류한다

                var listItems = from item in items
                                group item by item.price >= 10000 into g
                                select new { GroupKey = g.Key, Items = g };

                //listItems는 IGrouping<T>의 형식이다
                foreach (var group in listItems)
                {
                    Console.WriteLine(group.GroupKey ? "10000원이상" : "10000원 미만");
                    Console.WriteLine("======================================");
                    foreach (var item in group.Items)
                    {
                        Console.WriteLine($"{item.name}({item.price.ToString("#,#0")}원)");
                    }
                    Console.WriteLine();
                }


                int price = 1000000;
                string str = price.ToString("#,#0");
                //,사라지게하기
                str = str.Replace(',', '\0');
                Console.WriteLine(str);

                //문자열 편집.
                str = "Hello World! Good Bye";
                Console.WriteLine($"str에 Hello 있나요? : {str.Contains("Hello")}");        // 포함확인
                Console.WriteLine($"Good은 몇번째부터 시작하나요?:{str.IndexOf("Good")}");   //찾기
                Console.WriteLine($"잘라낸 문자열은?:{str.Substring(13)}");                 // 문자 잘라내기
                Console.WriteLine($"원래 문자는? : {str}");

                Console.WriteLine($"hello는 존재하나요? : {str.Contains("hello")}");
                Console.WriteLine($"모두 대문자로 하면?  :{str.ToUpper()} ");
                Console.WriteLine($"모두 소문자로 하면?  :{str.ToLower().Contains("hello")} ");

                str = "Good moring everyone.";
                Console.WriteLine($"공백 제거하면 : {str.Trim()}");
                Console.WriteLine($"morning을 제거하면 : {str.Trim().Remove(5, 8)}");                //index 5부터 8글자 지우겠다

            }
            if (false)
            {

                List<string> smiList = new List<string>();
                StreamReader reader = new StreamReader("smiText.txt");
                while (!reader.EndOfStream)
                    smiList.Add(reader.ReadLine());
                reader.Close();

                foreach (string smi in smiList)
                {
                    if (smi.Contains("SYNC"))
                    {
                        int syncEndIndex = smi.IndexOf('>');
                        int snycStartIndex = 12;
                        int count = syncEndIndex - syncEndIndex;
                        string time = smi.Substring(syncEndIndex, count);
                        Console.WriteLine($"추출한 시간 값은 : {time}");
                    }
                    //Console.WriteLine(smi);
                }
            }

            //확장 메서드
            int[] array = { 70, 20, 50, 40, 10, 30 };


            //배열 정렬
            array = array.Sort();

            //출력                   
            Console.WriteLine(array.TostringArray());             //원래 없는건데 확장매서드를 이용하여 확장했으므로 명령어로 뜬다
            //Console.WriteLine(array.ToString());
            Console.WriteLine(string.Join(',', array));

            //string형 자료형에 메서드확장
            //int ToInt() :문자열을 int형으로 바꿔주는데 바꿀수 없다면 -1을 반환하시오

            string levelStr = "123-";
            bool isValid = false;
            int level = levelStr.ToInt(out isValid);
            Console.WriteLine($"{level}({isValid})");


        }
    }
    //확장 메서드
    //static 클래스는 static 함수,변수만 가질수 있다
    public static class Method
    {
        public static int[] Sort(this int[] array, bool isAscending = true)     //오버라이딩한거랑 비슷한 효과를 낼수있다
        {
            if (isAscending)
                return array.OrderBy(num => num).ToArray();
            else
                return array.OrderByDescending(num => num).ToArray();
        }

        public static string TostringArray(this int[] array, char separator = ',')            //static뒤 string이 붙는데 그냥 static이 아니다 (확장매서드 부분)뒤에 추가가능하다 여기에 선택지를 주어 
        {                                                                                     //기본적으로 쉼표가 들어가서 가능하다
            return string.Join(',', array);
            /* string arrayStr = string.Empty;
             foreach (int num in array)
                 arrayStr += ($"{num},");
             arrayStr = arrayStr.Remove(arrayStr.Length - 1, 1);
             return arrayStr;*/
        }
        //out 키워드가 붙은 매개변수.
        // = >함수 내에서는 값의 대입을 강제하고 실제 매개변수에게 값을 대입시킨다.
        public static int ToInt(this string str, out bool isValid)
        {

            int value = -1;
            try
            {
                value = int.Parse(str);
                isValid = true;
            }
            catch (Exception e)
            {
                isValid = false;
            }

            return value;
        }

    }
}
