// <copyright file="InfoItemCard.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LlamaJournal.Models
{
    public class InfoItemCard
    {
        public InfoItemCard(string subject, string group)
        {
            this.Subject = subject;
            this.Group = group;
        }

        public string Subject { get; set; }

        public string Group { get; set; }
    }
}
