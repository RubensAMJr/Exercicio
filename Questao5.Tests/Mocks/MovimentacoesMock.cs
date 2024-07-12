using Questao5.Application.Commands.ContaCorrente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao5.Tests.Mocks
{
    public static class MovimentacoesMock
    {

        public static MovimentacaoCommandResult MovimentacaoMockSucess()
        {
            return new MovimentacaoCommandResult { IsSucess = true, IdMovimentacao = "B4C6A73D-1E59-4D21-B5B6-08E5A67C9E2", Message = "" };
        }

    }
}
