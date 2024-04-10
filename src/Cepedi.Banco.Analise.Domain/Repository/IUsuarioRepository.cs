﻿using Cepedi.Banco.Analise.Domain.Entities;

namespace Cepedi.Banco.Analise.Domain.Repository;

public interface IUsuarioRepository
{
    Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario);
    Task<UsuarioEntity> ObterUsuarioAsync(int id);

    Task<UsuarioEntity> AtualizarUsuarioAsync(UsuarioEntity usuario);
}
