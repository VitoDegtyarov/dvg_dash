﻿using AutoMapper;
using dvg.Data.Entities;
using dvg.Data.UnitOfWork;
using dvg.Dto;
using dvg.Services.Interfaces;

namespace dvg.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ClientDto>> GetAllAsync()
        {
            var data = await _unitOfWork.ClientRepository.GetAllAsync();
            
            List<ClientDto> result = _mapper.Map<List<ClientDto>>(data);

            return result;
        }

        public async Task<ClientDto> GetByIdAsync(Guid id)
        {
            var data = await _unitOfWork.ClientRepository.GetByIdAsync(id);

            var result = _mapper.Map<ClientDto>(data);

            return result;
        }

        public async Task InsertASync(ClientDto client)
        {
            if (client != null)
            {
                await _unitOfWork.ClientRepository.InsertAsync(_mapper.Map<Client>(client));

            }
        }

        public async Task DeleteClientAsync(ClientDto clientDto)
        {
            _unitOfWork.ClientRepository.Delete(_mapper.Map<Client>(clientDto));

            await _unitOfWork.SaveChanges();

        }
    }
}
