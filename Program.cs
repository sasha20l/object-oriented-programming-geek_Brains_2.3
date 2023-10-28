using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace homework2ObjectOrientedProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasl1andTask2();
            Task3Async();
            Console.ReadLine();
        }
        static void Tasl1andTask2()
        {
            BankAccount bankAccount1 = new BankAccount();
            bankAccount1.accountNumber = 123456;
            bankAccount1.balance = 1000;
            bankAccount1.SetbankAccountType("Checking");
            Console.WriteLine($"Номер Вашего счета набитый вручную {bankAccount1.accountNumber}, Номер Вашего счета сформированный автоматически {bankAccount1.GetAccountNumberGenerator()},  Ваш баланс {bankAccount1.balance}, тип счета {bankAccount1.GetbankAccountType()}");
            Console.ReadLine();

            BankAccount bankAccount2 = new BankAccount();
            bankAccount2.accountNumber = 654321;
            bankAccount2.balance = 3000;
            bankAccount2.SetbankAccountType("Savings");
            Console.WriteLine($"Номер Вашего счета набитый вручную {bankAccount2.accountNumber}, Номер Вашего счета сформированный автоматически {bankAccount2.GetAccountNumberGenerator()},  Ваш баланс {bankAccount2.balance}, тип счета {bankAccount2.GetbankAccountType()}");
            Console.ReadLine();

            BankAccount bankAccount3 = new BankAccount(1000, "Checking");
            Console.WriteLine($"Номер Вашего счета набитый вручную {bankAccount3.accountNumber}, Номер Вашего счета сформированный автоматически {bankAccount3.GetAccountNumberGenerator()},  Ваш баланс {bankAccount3.balance}, тип счета {bankAccount3.GetbankAccountType()}");
            Console.ReadLine();

            bankAccount2.BankAccountOperation(bankAccount1, 800);
            bankAccount2.BankAccountOperation(bankAccount1, 800);

            Console.WriteLine(bankAccount1.ReverceString(Console.ReadLine()));
            Console.ReadLine();
        }
        static async Task Task3Async()
        {
            string path = @"D:\task.txt";  
            string path1 = @"D:\task1.txt";

            string textFromFile;

            using (FileStream fstream = File.OpenRead(path))
            {

                byte[] buffer = new byte[fstream.Length];

                await fstream.ReadAsync(buffer, 0, buffer.Length);

                textFromFile = Encoding.Default.GetString(buffer);

                Regex regex = new Regex(@"(\S*)@(\S*).ru");

                MatchCollection matches = regex.Matches(textFromFile);

                textFromFile = "";

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                        textFromFile += " " + match;
                }
                else
                {
                    Console.WriteLine("Совпадений не найдено");
                }

            }

            using (FileStream fstream = new FileStream(path1, FileMode.OpenOrCreate))
            {

                byte[] buffer = Encoding.Default.GetBytes(textFromFile);

                await fstream.WriteAsync(buffer, 0, buffer.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }
    } 

    class BankAccount
    {
        public int accountNumber { get; set; }

        public int balance { get; set; }

        private static int accountNumberGenerator = 0;

        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public BankAccount(string accountType)
        {
            SetbankAccountType(accountType);
            accountNumberGenerator++;
        }

        public BankAccount(int balance, string accountType)
        {
            this.balance = balance;
            SetbankAccountType(accountType);
            accountNumberGenerator++;
        }


        public enum BankAccountType
        {
            Checking,
            Savings
        }

        private BankAccountType bankAccountType;

        public BankAccount() { accountNumberGenerator++;}

        public void BankAccountOperation(BankAccount bankAccount, int summ)
        {
            if (bankAccount.Withdraw(summ))
            {
                Todeposit(summ);
                Console.WriteLine($"Со счета {bankAccount} переведено на счет {this}, {summ} рублей");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"На счете {bankAccount}, нету {summ} рублей");
                Console.ReadLine();

            }

        }

        public int GetAccountNumberGenerator()
        {
            return accountNumberGenerator;
        }

        public void SetbankAccountType(string accountType)
        {
            if (accountType == "Checking")
            {
                bankAccountType = BankAccountType.Checking;
            }else if (accountType == "Savings")
            {
                bankAccountType = BankAccountType.Savings;
            }
            
        }

        public BankAccountType GetbankAccountType()
        {
            return bankAccountType;
        }

        public bool Withdraw(int many)
        {
            if (many <= balance)
            {
                balance -= many;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void Todeposit(int many)
        {
            balance += many;
        }

        public string ReverceString(string str)
        {
            var reverStr = str.ToCharArray();
            Array.Reverse(reverStr);
            return new string(reverStr);
        }


    }
}
