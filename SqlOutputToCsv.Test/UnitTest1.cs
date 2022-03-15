using System;
using System.Collections.Generic;
using Xunit;

namespace SqlOutputToCsv.Test
{
    public class UnitTest1
    {
        [Fact]
        public void EmptyListTest()
        {
            List<string> linhas = new List<string>();
            var result = new ConversorSqlQueryOutputParaCsv().Converter(linhas);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void NullListTest()
        {
            Assert.Throws<NullReferenceException>(() => new ConversorSqlQueryOutputParaCsv().Converter(null));
        }

        [Fact]
        public void SpacesInDataTest()
        {
            List<string> linhas = new List<string>();
            linhas.Add("MemberId   ,Surname        ,FirstName      ,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n");
            linhas.Add("-----------,---------------,---------------,-----------,-------------------,------------------,-------------------\r\n");
            linhas.Add("1          ,Downs          ,Graham         ,1980-12-14 ,1998-01-01         ,1                 ,5120      \r\n");
            linhas.Add("2          ,Blogs          ,Joe            ,1978-03-20 ,2012-06-01         ,0                 ,2965      \r\n");
            linhas.Add("3          ,Smith          ,Jenny          ,1994-01-01 ,2005-12-01         ,11                ,31658     \r\n");
            linhas.Add("4          ,Van Der Merwe  ,Jan            ,1954-09-30 ,1980-01-01         ,2                 ,10300     \r\n");
            linhas.Add("\r\n");
            linhas.Add("(4 row(s) affected)\r\n");

            var result = new ConversorSqlQueryOutputParaCsv().Converter(linhas);
            Assert.Equal(
                "MemberId,Surname,FirstName,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n" +
                "1,Downs,Graham,1980-12-14,1998-01-01,1,5120\r\n" +
                "2,Blogs,Joe,1978-03-20,2012-06-01,0,2965\r\n" +
                "3,Smith,Jenny,1994-01-01,2005-12-01,11,31658\r\n" +
                "4,Van Der Merwe,Jan,1954-09-30,1980-01-01,2,10300\r\n", result);
        }

        [Fact]
        public void SingleRowTest()
        {

            List<string> linhas = new List<string>();
            linhas.Add("MemberId   ,Surname        ,FirstName      ,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n");
            linhas.Add("-----------,---------------,---------------,-----------,-------------------,------------------,-------------------\r\n");
            linhas.Add("1          ,Downs          ,Graham         ,1980-12-14 ,1998-01-01         ,1                 ,5120      \r\n");
            linhas.Add("\r\n");
            linhas.Add("(1 row(s) affected)\r\n");

            var result = new ConversorSqlQueryOutputParaCsv().Converter(linhas);
            Assert.Equal(
              "MemberId,Surname,FirstName,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n" +
              "1,Downs,Graham,1980-12-14,1998-01-01,1,5120\r\n", result);
        }

        [Fact]
        public void DashesInDataTest()
        {
            List<string> linhas = new List<string>();
            linhas.Add("MemberId   ,Surname        ,FirstName      ,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n");
            linhas.Add("-----------,---------------,---------------,-----------,-------------------,------------------,-------------------\r\n");
            linhas.Add("5          ,---------------,---------------,---------- ,----------         ,----------        ,----------\r\n");
            linhas.Add("\r\n");
            linhas.Add("(1 row(s) affected)\r\n");

            var result = new ConversorSqlQueryOutputParaCsv().Converter(linhas);
            Assert.Equal(
              "MemberId,Surname,FirstName,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n" +
              "5,---------------,---------------,----------,----------,----------,----------\r\n", result);
        }

        [Fact]
        public void NullValuesInDataTest()
        {
            List<string> linhas = new List<string>();
            linhas.Add("MemberId   ,Surname        ,FirstName      ,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n");
            linhas.Add("-----------,---------------,---------------,-----------,-------------------,------------------,-------------------\r\n");
            linhas.Add("6          ,Doe            ,John           ,NULL       ,2017-06-01         ,0                 ,3500      \r\n");
            linhas.Add("\r\n");
            linhas.Add("(1 row(s) affected)\r\n");

            var result = new ConversorSqlQueryOutputParaCsv().Converter(linhas);
            Assert.Equal(
              "MemberId,Surname,FirstName,DateOfBirth,MembershipStartDate,NumberOfDependents,MonthlyPremiumRands\r\n" +
              "6,Doe,John,,2017-06-01,0,3500\r\n", result);
        }
    }
}
