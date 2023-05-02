// <copyright file="AnalysisUserModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LlamaJournal.Models
{
    using DataLayer.Models;

    public class AnalysisUserModel
    {
        public AnalysisUserModel(User user)
        {
            this.Id = user.Id;
            this.FullName = user.FullName;
            this.Group = user.Group.Name;
        }

        public string Id { get; set; }

        public string FullName { get; set; }

        public string Group { get; set; }
    }
}