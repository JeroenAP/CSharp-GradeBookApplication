﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var threshold = (int)Math.Ceiling(Students.Count * 0.2);

            var allGrades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
            var indexLocation = allGrades.FindIndex(item => item < averageGrade);

            switch (indexLocation + 1)
            {
                case var n when n > allGrades.Count - threshold:
                    return 'D';
                case var n when n > allGrades.Count - (threshold * 2):
                    return 'C';
                case var n when n > allGrades.Count - (threshold * 3):
                    return 'B';
                case var n when n > allGrades.Count - (threshold * 4):
                    return 'A';
                default:
                    return 'F';
            }
        }
    }
}
