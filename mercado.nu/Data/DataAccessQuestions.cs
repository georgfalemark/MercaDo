﻿using mercado.nu.Models.Entities;
using mercado.nu.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mercado.nu.Data
{
    public class DataAccessQuestions
    {
        public ApplicationDbContext _questionContext;

        public DataAccessQuestions(ApplicationDbContext questionContext)
        {
            _questionContext = questionContext;
        }

        internal async Task SaveChapter(Guid marketResearchId, Chapters chapters)
        {

            var marketResearch = _questionContext.MarketResearches.Single(x => x.MarketResearchId == marketResearchId);
            chapters.MarketResearch = marketResearch;
            chapters.ChaptersId = Guid.NewGuid();
            _questionContext.Add(chapters);
            await _questionContext.SaveChangesAsync();

        }

        internal List<Chapters> GetChapters(Guid marketResearchId)
        {
            
            var chapters = _questionContext.Chapters.Where(x => x.MarketResearch.MarketResearchId == marketResearchId).ToList();
            return chapters;
        }

        internal async Task<Guid> saveQuestion(AddQuestionToMarketResearchVm questionToMarketResearchVm)
        {
            questionToMarketResearchVm.Question.QuestionId = Guid.NewGuid();
            _questionContext.Add(questionToMarketResearchVm.Question);
            await _questionContext.SaveChangesAsync();
            return questionToMarketResearchVm.Question.QuestionId;
        }

        internal async Task AddQuestionOption(QuestionOption questionOption)
        {
            _questionContext.Add(questionOption);
            await _questionContext.SaveChangesAsync();
        }
    }
}
