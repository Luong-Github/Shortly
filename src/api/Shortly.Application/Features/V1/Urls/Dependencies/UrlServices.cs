using Microsoft.Extensions.Configuration;
using Shortly.Contract.Dependencies.DataAccess;
using Shortly.Contract.Dependencies.Services;
using Shortly.Domain.Entities;
using Shortly.Share.Constants;
using Shortly.Share.Utilities;

namespace Shortly.Application.Features.V1.Urls.Dependencies
{
    public class UrlServices : IUrlServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;

        private readonly int _keyLength;

        // Constructor: Dependency Injection
        public UrlServices(IUnitOfWork unitOfWork, ILoggerManager logger, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Initialize configuration values, with fallback value if not set
            _keyLength = int.TryParse(_configuration.GetSection("ShortenerSettings:KeyLength").Value, out var keyLength)
                ? keyLength
                : 6; // Default value of 6 if configuration isn't set or invalid
        }

        public async Task<string> CreateShortenUrlAsync(string originalUrl)
        {
            string shortenedUrl = string.Empty;

            // Start the transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Generate a shortened URL
                shortenedUrl = GenerateShortenURL(originalUrl);

                await _unitOfWork.UrlRepository.AddAsync(new Url
                {
                    OriginalUrl = originalUrl,
                    ShortenUrl = shortenedUrl,
                    CreatedDate = DateTimeOffset.UtcNow
                });

                // Simulating another operation (like logging, updates, etc.)
                _logger.LogInfo($"Shortened URL created successfully.");

                // Save all changes and commit transaction
                await _unitOfWork.CommitTransactionAsync();

                return shortenedUrl;
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any exception
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError("An error occurred while creating shortened URL.", ex);

                throw new InvalidOperationException("Failed to save shortened URL. Changes rolled back.", ex);
            }
        }

        #region Private Methods

        // Private method: Generates a shortened URL
        private string GenerateShortenURL(string originalUrl)
        {
            // Hash the original URL (SHA-256 for collision-resistance)
            byte[] longUrlHash = HashGenerator.GenerateHashBytes(originalUrl, Share.Enums.HashAlgorithmTypeEnum.SHA256);

            // Extract a portion of the hash using the key length configuration
            byte[] hashSegment = ByteSegmentExtractor.Extract(longUrlHash, 0, _keyLength);

            // Convert hash to a decimal value
            long decimalValue = ValueConverter.HexByteToDecimal(hashSegment);

            // Encode to Base62
            string shortUrl = ToolboxUtils.EncodeToBase(decimalValue, EncodeDecodeBase.Base62Chars);

            return shortUrl;
        }

        #endregion
    }
}
