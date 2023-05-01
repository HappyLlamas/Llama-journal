// <copyright file="ProgressViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LlamaJournal.Models
{
    public class Card
    {
        public Card(string subject, string fullName, int totalGrades)
        {
            this.Subject = subject;
            this.FullName = fullName;
            this.TotalGrades = totalGrades;
        }

        public string Subject { get; set; }

        public string FullName { get; set; }

        public int TotalGrades { get; set; }
    }
}