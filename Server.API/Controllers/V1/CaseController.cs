using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Server.API.Helpers;
using Server.Core.Common;
using Server.Core.Entities.Common;
using Server.Core.Entities.DTO;
using Server.Core.Interfaces.IServices;

namespace Server.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CaseController : ControllerBase
    {
        private readonly ILogger<CaseController> _logger;
        private readonly ICaseService _caseService;
        private readonly IMemoryCache _memoryCache;

        public CaseController(ILogger<CaseController> logger, ICaseService caseService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _caseService = caseService;
            _memoryCache = memoryCache;
        }

        [HttpGet("paginated-data")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize, string? search, string? sortBy, string? sortOrder, CancellationToken cancellationToken)
        {
            try
            {
                int pageSizeValue = pageSize ?? 10;
                int pageNumberValue = pageNumber ?? 1;
                sortBy = sortBy ?? "Id";
                sortOrder = sortOrder ?? "desc";

                var filters = new List<ExpressionFilter>();
                if (!string.IsNullOrWhiteSpace(search) && search != null)
                {
                    // Add filters for relevant properties
                    filters.AddRange(new[]
                    {
                        new ExpressionFilter
                        {
                            PropertyName = "Code",
                            Value = search,
                            Comparison = Comparison.Contains
                        },
                        new ExpressionFilter
                        {
                            PropertyName = "Name",
                            Value = search,
                            Comparison = Comparison.Contains
                        },
                        new ExpressionFilter
                        {
                            PropertyName = "Description",
                            Value = search,
                            Comparison = Comparison.Contains
                        }
                    });

                    // Check if the search string represents a valid numeric value for the "Price" property
                    if (double.TryParse(search, out double price))
                    {
                        filters.Add(new ExpressionFilter
                        {
                            PropertyName = "Price",
                            Value = price,
                            Comparison = Comparison.Equal
                        });
                    }
                }

                var cases = await _caseService.GetPaginatedData(pageNumberValue, pageSizeValue, filters, sortBy, sortOrder, cancellationToken);

                var response = new ResponseDTO<PaginatedDataDTO<CaseDTO>>
                {
                    Success = true,
                    Message = "Cases retrieved successfully",
                    Data = cases
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving cases");

                var errorResponse = new ResponseDTO<IEnumerable<CaseDTO>>
                {
                    Success = false,
                    Message = "Error retrieving cases",
                    Error = new ErrorDTO
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var cases = await _caseService.GetAll(cancellationToken);

                var response = new ResponseDTO<IEnumerable<CaseDTO>>
                {
                    Success = true,
                    Message = "Cases retrieved successfully",
                    Data = cases
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving cases");

                var errorResponse = new ResponseDTO<IEnumerable<CaseDTO>>
                {
                    Success = false,
                    Message = "Error retrieving cases",
                    Error = new ErrorDTO
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var caseDTO = new CaseDTO();

                // Attempt to retrieve the case from the cache
                if (_memoryCache.TryGetValue($"Case_{id}", out CaseDTO cachedCase))
                {
                    caseDTO = cachedCase;
                }
                else
                {
                    // If not found in cache, fetch the case from the data source
                    caseDTO = await _caseService.GetById(id, cancellationToken);

                    if (caseDTO != null)
                    {
                        // Cache the case with an expiration time of 10 minutes
                        _memoryCache.Set($"Case_{id}", caseDTO, TimeSpan.FromMinutes(10));
                    }
                }

                var response = new ResponseDTO<CaseDTO>
                {
                    Success = true,
                    Message = "Case retrieved successfully",
                    Data = caseDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No data found")
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO<CaseDTO>
                    {
                        Success = false,
                        Message = "Case not found",
                        Error = new ErrorDTO
                        {
                            Code = "NOT_FOUND",
                            Message = "Case not found"
                        }
                    });
                }

                _logger.LogError(ex, $"An error occurred while retrieving the case");

                var errorResponse = new ResponseDTO<CaseDTO>
                {
                    Success = false,
                    Message = "Error retrieving case",
                    Error = new ErrorDTO
                    {
                        Code = "ERROR_CODE",
                        Message = ex.Message
                    }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CaseCreateDTO model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                string message = "";
                if (await _caseService.IsExists("Number", model.Number, cancellationToken))
                {
                    message = $"The case number- '{model.Number}' already exists";
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO<CaseDTO>
                    {
                        Success = false,
                        Message = message,
                        Error = new ErrorDTO
                        {
                            Code = "DUPLICATE_NUMBER",
                            Message = message
                        }
                    });
                }

                try
                {
                    var data = await _caseService.Create(model, cancellationToken);

                    var response = new ResponseDTO<CaseDTO>
                    {
                        Success = true,
                        Message = "Case created successfully",
                        Data = data
                    };

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while adding the case");
                    message = $"An error occurred while adding the case- " + ex.Message;

                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<CaseDTO>
                    {
                        Success = false,
                        Message = message,
                        Error = new ErrorDTO
                        {
                            Code = "ADD_ROLE_ERROR",
                            Message = message
                        }
                    });
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO<CaseDTO>
            {
                Success = false,
                Message = "Invalid input",
                Error = new ErrorDTO
                {
                    Code = "INPUT_VALIDATION_ERROR",
                    Message = ModelStateHelper.GetErrors(ModelState)
                }
            });
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(CaseUpdateDTO model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                string message = "";
                if (await _caseService.IsExistsForUpdate(model.Id, "Number", model.Number, cancellationToken))
                {
                    message = $"The case number- '{model.Number}' already exists";
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
                    {
                        Success = false,
                        Message = message,
                        Error = new ErrorDTO
                        {
                            Code = "DUPLICATE_NUMBER",
                            Message = message
                        }
                    });
                }

                try
                {
                    await _caseService.Update(model, cancellationToken);

                    // Remove data from cache by key
                    _memoryCache.Remove($"Case_{model.Id}");

                    var response = new ResponseDTO
                    {
                        Success = true,
                        Message = "Case updated successfully"
                    };

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while updating the case");
                    message = $"An error occurred while updating the case- " + ex.Message;

                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
                    {
                        Success = false,
                        Message = message,
                        Error = new ErrorDTO
                        {
                            Code = "UPDATE_ROLE_ERROR",
                            Message = message
                        }
                    });
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO
            {
                Success = false,
                Message = "Invalid input",
                Error = new ErrorDTO
                {
                    Code = "INPUT_VALIDATION_ERROR",
                    Message = ModelStateHelper.GetErrors(ModelState)
                }
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _caseService.Delete(id, cancellationToken);

                // Remove data from cache by key
                _memoryCache.Remove($"Case_{id}");

                var response = new ResponseDTO
                {
                    Success = true,
                    Message = "Case deleted successfully"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No data found")
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO
                    {
                        Success = false,
                        Message = "Case not found",
                        Error = new ErrorDTO
                        {
                            Code = "NOT_FOUND",
                            Message = "Case not found"
                        }
                    });
                }

                _logger.LogError(ex, "An error occurred while deleting the case");

                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
                {
                    Success = false,
                    Message = "Error deleting the case",
                    Error = new ErrorDTO
                    {
                        Code = "DELETE_ROLE_ERROR",
                        Message = ex.Message
                    }
                });

            }
        }
    }
}
