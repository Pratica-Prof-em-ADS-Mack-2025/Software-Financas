using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Util
{
    using core.Domain.Entities;
    using Microsoft.CodeAnalysis.Options;
    using System.Net;
    using System.Net.Mail;
    public class Email
    {

        public bool EnviaEmail(string html, string assunto, string destinatario)
        {
            Util.Settings app = new Util.Settings();
            try
            {
                SmtpClient smtpClient = new SmtpClient(app.Appsettings("smtp"), int.Parse(app.Appsettings("porta")));
                smtpClient.EnableSsl = false;
                MailMessage message = new MailMessage(new MailAddress(app.Appsettings("Email"), assunto), new MailAddress(destinatario, assunto));
                message.IsBodyHtml = true;
                string str = html;
                message.Body = str;
                message.Subject = assunto;
                NetworkCredential networkCredential = new NetworkCredential(app.Appsettings("Email"), app.Appsettings("Senha"), "");
                smtpClient.Credentials = (ICredentialsByHost)networkCredential;
                Console.WriteLine("Enviando...");

                smtpClient.Send(message);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public string EnviaRedefinirSenha(string nome, string senha)
        {
            return $@"Olá {nome}, sua nova senha é {senha}, aconselhamos a redefinir a senha o quanto antes pois ela vai expirar em 2 dias.";
        }

        public string EnviaAssinou(string nome, string documento)
        {
            return $@"O usuário {nome} assinou o documento: '{documento}'";
        }

        public string EnviarSenha(string nome, string user, string email, string senha)
        {
            return $@"Olá <strong>{nome}</strong>,<br/>Bem vindo ao Polaris!<br>Seu usuário:<strong>{user}</strong> <br>Email cadastrado: <strong>{email}</strong><br>Sua senha:<strong>{senha}</strong><br/><br/><strong>Por motivos de segurança aconselhamos a redefinir a senha o quanto antes.</strong> <br> Acesse o painel clicando <a href='https://polaris.caixaassistencia.com.br/'>aqui.</a>";
        }

        public string EnviaEmailSolicitacaoAcesso(string tITULO, DateTime dT_ACESSO)
        {
            return $@"Uma solicitção de acesso assistido com o título:<br> {tITULO},<br> foi inserida com data para {dT_ACESSO.ToString("dd/MM/yyyy HH:mm")}";
        }

        public string EnviaEmailAberturaChamadoLumini(string Problema, string Tipo, string Descricao, string nome, string email, string prioridade, DateTime dtAbertura)
        {

            if (prioridade == "00")
            {
                prioridade = "Baixa";
            }
            else if (prioridade == "01")
            {
                prioridade = "Média";
            }
            else if (prioridade == "02")
            {
                prioridade = "Alta";
            }
            else if (prioridade == "03")
            {
                prioridade = "Crítica";
            }
            if (Problema == "Solicitacao")
            {
                return $@"<br/>Prezados,<br/><br/>Favor verificar o chamado abaixo:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo:</strong> {Tipo}<br/>
                        <strong>Solicitação:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
    
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}";
            }
            else
            {
                return $@"<br/>Prezados,<br/><br/>Favor verificar o chamado abaixo:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo do Problema:</strong> {Tipo}<br/>
                        <strong>Problema:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
    
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}";
            }

        }

        public string EnviaEmailAberturaChamadoLuminiRH(string Tipo, string Descricao, string nome, string prioridade, DateTime dtAbertura)
        {

            if (prioridade == "00")
            {
                prioridade = "Baixa";
            }
            else if (prioridade == "01")
            {
                prioridade = "Média";
            }
            else if (prioridade == "02")
            {
                prioridade = "Alta";
            }
            else if (prioridade == "03")
            {
                prioridade = "Crítica";
            }

                return $@"<br/>Prezados,<br/><br/>Favor verificar o chamado abaixo:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo do Problema:</strong> {Tipo}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
    
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}";
            

        }

        public string EnviaEmailAberturaChamadoPosVendas(string Problema, string Tipo, string Descricao, string nome, string email, string prioridade, DateTime dtAbertura)
        {

            if (prioridade == "00")
            {
                prioridade = "Baixa";
            }
            else if (prioridade == "01")
            {
                prioridade = "Média";
            }
            else if (prioridade == "02")
            {
                prioridade = "Alta";
            }
            else if (prioridade == "03")
            {
                prioridade = "Crítica";
            }
            if (Problema == "Solicitacao")
            {
                return $@"<br/>Prezados,<br/><br/>Favor verificar o chamado abaixo:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo:</strong> {Tipo}<br/>
                        <strong>Solicitação:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
    
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}";
            }
            else
            {
                return $@"<br/>Prezados,<br/><br/>Favor verificar o chamado abaixo:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo do Problema:</strong> {Tipo}<br/>
                        <strong>Problema:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
    
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}";
            }

        }

        public string EnviaEmailRedirecionamenoChamadoPosVendas(string Problema, string Tipo, string Descricao, string nome, string email, string prioridade, DateTime dtAbertura, string descricao, string setor, string responsavel)
        {

            if (prioridade == "00")
            {
                prioridade = "Baixa";
            }
            else if (prioridade == "01")
            {
                prioridade = "Média";
            }
            else if (prioridade == "02")
            {
                prioridade = "Alta";
            }
            else if (prioridade == "03")
            {
                prioridade = "Crítica";
            }
            if (Problema == "Solicitacao")
            {
                return $@"<br/>Prezados,<br/><br/>Chamado redirecionado para atendimento, favor abrir a Intranet para as devidas tratativas:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo:</strong> {Tipo}<br/>
                        <strong>Solicitação:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}
                        <br/><br/>
                        <strong> Chamado redirecionado pelo:</strong> {responsavel}<br/>
                        <strong> Descrição:</strong> {descricao}<br/>
                        <strong> Departamento:</strong> {setor}<br/><br/><br/>
                        <i>Menu Solicitações/Chamados Pós Vendas/Chamado Pós Vendas para Atendimento</i>";
            }
            else
            {
                return $@"<br/>Prezados,<br/><br/>Chamado redirecionado para atendimento, favor abrir a Intranet para as devidas tratativas:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo:</strong> {Tipo}<br/>
                        <strong>Problema:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}
                        <br/><br/>
                        <i>Redirecionamento</i><br/>
                        <strong> Chamado redirecionado pelo:</strong> {responsavel}<br/>
                        <strong> Descrição:</strong> {descricao}<br/>
                        <strong> Departamento:</strong> {setor}<br/><br/><br/>
                        <i>Menu Solicitações/Chamados Pós Vendas/Chamado Pós Vendas para Atendimento</i>";
            }

        }

        public string EnviaEmailRedirecionamenoOcorrencia(string Problema, string Tipo, string Descricao, string nome, string email, string prioridade, DateTime dtAbertura, string descricao, string setor, string responsavel)
        {

            if (prioridade == "00")
            {
                prioridade = "Baixa";
            }
            else if (prioridade == "01")
            {
                prioridade = "Média";
            }
            else if (prioridade == "02")
            {
                prioridade = "Alta";
            }
            else if (prioridade == "03")
            {
                prioridade = "Crítica";
            }
            if (Problema == "Solicitacao")
            {
                return $@"<br/>Prezados,<br/><br/>Chamado redirecionado para atendimento, favor abrir a Intranet para as devidas tratativas:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo:</strong> {Tipo}<br/>
                        <strong>Solicitação:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}
                        <br/><br/>
                        <strong> Chamado redirecionado pelo:</strong> {responsavel}<br/>
                        <strong> Descrição:</strong> {descricao}<br/>
                        <strong> Departamento:</strong> {setor}<br/><br/><br/>
                        <i>Menu Solicitações/Chamados Pós Vendas/Chamado Pós Vendas para Atendimento</i>";
            }
            else
            {
                return $@"<br/>Prezados,<br/><br/>Chamado redirecionado para atendimento, favor abrir a Intranet para as devidas tratativas:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo:</strong> {Tipo}<br/>
                        <strong>Problema:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
                        <br/>
                        <strong>Nome Solicitante:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}
                        <br/><br/>
                        <i>Redirecionamento</i><br/>
                        <strong> Chamado redirecionado pelo:</strong> {responsavel}<br/>
                        <strong> Descrição:</strong> {descricao}<br/>
                        <strong> Departamento:</strong> {setor}<br/><br/><br/>
                        <i>Menu Solicitações/Chamados Pós Vendas/Chamado Pós Vendas para Atendimento</i>";
            }

        }

        public string EnviaEmailAceiteSolicitacaoAcesso(string tITULO, DateTime dT_ACESSO)
        {
            return $@"A sua solicitção com o título:<br> {tITULO},<br> foi aceita para {dT_ACESSO.ToString("dd/MM/yyyy HH:mm")}, aguarde o email com a sala de reunião.";
        }

        internal string EnviaEmailAceiteSolicitacaoAcessoGerencia(string tITULO, string dESCRICAO, DateTime dT_ACESSO, string nOME, string eMAIL, string sISTEMA)
        {
            return $@"A solicitção com o título:<br> {tITULO},<br>Descrição:<br>{dESCRICAO} <br>do sistema:<br>{sISTEMA}<br> foi aceita para a data {dT_ACESSO.ToString("dd/MM/yyyy HH:mm")}, favor enviar para o usuário {nOME}, um email ({eMAIL}) combinando a sala onde será assistido o acesso.";
        }

        public string EnviaComprovante(string url)
        {
            return $@"Olá! O seu comprovante do Rapidex pode ser baixado <a href='{url}'>aqui</a>.";
        }
        public string EnviaEmailAberturaOcorrencia(string Problema, string Tipo, string Descricao, string nome, string email, string prioridade, DateTime dtAbertura)
        {

            if (prioridade == "00")
            {
                prioridade = "Baixa";
            }
            else if (prioridade == "01")
            {
                prioridade = "Média";
            }
            else if (prioridade == "02")
            {
                prioridade = "Alta";
            }
            else if (prioridade == "03")
            {
                prioridade = "Crítica";
            }

                return $@"<br/>Prezados,<br/><br/>Foi gerada a seguinte ocorrência:<br/><br/>
                        <strong>Prioridade:</strong> {prioridade}<br/>
                        <strong>Tipo da Ocorrência:</strong> {Tipo}<br/>
                        <strong>Problema:</strong> {Problema}<br/>
                        <strong>Descrição:</strong> {Descricao}<br/>
    
                        <br/>
                        <strong>Ocorrência gerada por:</strong> {nome}<br/>
                        <strong>E-mail:</strong> {email}<br/>
                        <strong>Data Abertura:</strong> {dtAbertura.ToString("dd/MM/yyyy HH:mm")}";

        }

    }
}
