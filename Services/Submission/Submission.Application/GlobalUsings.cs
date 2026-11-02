global using Articles.Abstractions;
global using Articles.Abstractions.Enums;
global using FluentValidation;
global using MediatR;
global using Submission.Domain.Entities;
global using Submission.Persistence.Repositories;

global using AssetTypeDefinationRepository = Blocks.EntityFramework.CashedRepository<
    Submission.Persistence.Data.SubmissionDbContext,
    Submission.Domain.Entities.AssetTypeDefination,
    Articles.Abstractions.Enums.AssetType>;
