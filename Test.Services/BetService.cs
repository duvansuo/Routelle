using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Extensions;
using Test.Models;
using Test.Repository.Interfaces;
using Test.Services.Interfaces;

namespace Test.Services
{
    public class BetService : IBetService
    {
        readonly IBetRepository _IBetRepository;
        readonly IRouletteService _IRouletteService;
        private readonly IMapper _mapper;
        readonly ResultBetDto resultBets = new ResultBetDto();
        public BetService(IBetRepository repository, IRouletteService iRouletteService, IMapper mapper)
        {
            _mapper = mapper;
            _IBetRepository = repository;
            _IRouletteService = iRouletteService;
        }
        public async Task<bool> Create(BetDto betDto)
        {
            try
            {
                betDto.BetId = Guid.NewGuid();
                var NewBetId = await _IBetRepository.Create(betDto) ? betDto.BetId.ToString() : throw new Exception(Constants.ERRORCREATE);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ValidateDto> ValidateBet(BetDto betDto)
        {
            ValidateDto validateDto = new ValidateDto();
            if (string.IsNullOrEmpty(betDto.UserId)) validateDto.Mesage += $"-{Constants.USERNULL}";

            if (!ValidateColor(betDto.Color)) validateDto.Mesage += $"-{Constants.INVALIDCOLOR}";
            if (!string.IsNullOrEmpty(betDto.Color) && betDto.Number != 0) validateDto.Mesage += $"-{Constants.NUMBERORCOLOR}";
            if (!await _IRouletteService.ValidRoulette(betDto.RouletteId)) validateDto.Mesage += $"-{Constants.ROULETTECLOSED }";
            validateDto.Status = string.IsNullOrEmpty(validateDto.Mesage);
            return validateDto;
        }
        private bool ValidateColor(string color)
        {
            if (string.IsNullOrEmpty(color)) return true;
            switch (color.ToLower())
            {
                case "negro":
                case "rojo":
                    return true;
                default:
                    return false;
            }
        }
        public async Task<ResultBetDto> CloseBet(Guid rouletteId)
        {
            try
            {
                await _IRouletteService.ChangeStateRoulette(rouletteId, false);
                List<BetDto> listBetDto = await GetAllByRouletteId(rouletteId);
                List<WinningBetDto> winningBetDto = GetWinner(listBetDto);
                resultBets.ListBetDto = listBetDto;
                resultBets.ListWinningBetDto = winningBetDto;
                return resultBets;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<List<BetDto>> GetAllByRouletteId(Guid rouletteId)
        {
            List<BetDto> listbetDto = await _IBetRepository.GetAll();
            return listbetDto.Where(x => x.RouletteId == rouletteId).ToList();
        }
        private List<WinningBetDto> GetWinner(List<BetDto> listBetDto)
        {
            //int WinningNumber = new Random().Next(0, 37);
            int WinningNumber = 21;
            resultBets.WinningColor = (WinningNumber % 2 == 0 ? Constants.RED : Constants.BLACK);
            resultBets.WinningNumber = (short)WinningNumber;
            List<WinningBetDto> winningBetDto = CalculateProfitByNumber(listBetDto.Where(x => x.Number == WinningNumber).ToList());
            winningBetDto.AddRange(CalculateProfitColor(listBetDto.Where(x => x.Color.ToLower() == resultBets.WinningColor.ToLower()).ToList()));
            return winningBetDto;
        }
        private List<WinningBetDto> CalculateProfitByNumber(List<BetDto> WinningbetByNumber)
        {
            List<WinningBetDto> listWinning = new List<WinningBetDto>();
            foreach (var item in WinningbetByNumber)
            {
                WinningBetDto Winning = JsonConvert.DeserializeObject<WinningBetDto>(JsonConvert.SerializeObject(item));
                Winning.AmountEarned = item.Amount * 5;
                Winning.WonBy = Constants.NUMBER;
                listWinning.Add(Winning);
            }
            return listWinning;
        }
        private List<WinningBetDto> CalculateProfitColor(List<BetDto> WinningbetByColor)
        {
            List<WinningBetDto> listWinning = new List<WinningBetDto>();
            foreach (var item in WinningbetByColor)
            {
                WinningBetDto Winning = JsonConvert.DeserializeObject<WinningBetDto>(JsonConvert.SerializeObject(item));
                Winning.AmountEarned = item.Amount * 1.8;
                Winning.WonBy = Constants.COLOR;
                listWinning.Add(Winning);
            }
            return listWinning;
        }
    }
}