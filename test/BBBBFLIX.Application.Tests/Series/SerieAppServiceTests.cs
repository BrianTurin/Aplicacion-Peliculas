using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BBBBFLIX.EntityFrameworkCore;
using Moq;
using NSubstitute;
using BBBBFLIX.Series;
using BBBBFLIX.Users;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Xunit;
using Volo.Abp.ObjectMapping;
using Autofac.Core;
using System.Linq;
using Volo.Abp.Domain.Entities;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using BBBBFLIX;
using Microsoft.Extensions.Logging;
using BBBBFLIX.Monitoring;

public class SerieAppServiceTests
{
    private readonly Mock<IRepository<Serie, int>> _serieRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<ISeriesApiService> _seriesApiServiceMock;
    private readonly Mock<IObjectMapper> _objectMapper;
    private readonly Mock<IAPIMonitoringAppService> _monitoringAppService;
    private readonly Mock<ILogger<SerieAppService>> _loggerMock; // Corrected type  
    private readonly SerieAppService _serieAppService;

    public SerieAppServiceTests()
    {
        _serieRepositoryMock = new Mock<IRepository<Serie, int>>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _seriesApiServiceMock = new Mock<ISeriesApiService>();
        _objectMapper = new Mock<IObjectMapper>();
        _monitoringAppService = new Mock<IAPIMonitoringAppService> { };
        _loggerMock = new Mock<ILogger<SerieAppService>>();
        _serieAppService = new SerieAppService(
            _serieRepositoryMock.Object,
            _seriesApiServiceMock.Object,
            _currentUserServiceMock.Object,
            _objectMapper.Object,
            _loggerMock.Object,
            _monitoringAppService.Object
        );
    }
}

