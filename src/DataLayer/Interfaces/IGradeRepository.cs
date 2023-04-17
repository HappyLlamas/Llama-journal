﻿using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IGradeRepository
{
    Grade? GetById(long id);
    List<Grade> GetGradesForUser(Discipline discipline, User user);
    List<Grade> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime startDatetime, DateTime endDatetime);
    List<Grade> GetGradesForGroup(Discipline discipline, Group group);
    void AddGrade(string userId, int score, DateTime date);
    void SetGradeComment(Grade grade, string comment);
}
}
