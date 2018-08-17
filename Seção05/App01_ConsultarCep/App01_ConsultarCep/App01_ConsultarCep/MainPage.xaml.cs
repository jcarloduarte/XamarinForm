using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCep.Servico.Modelo;
using App01_ConsultarCep.Servico;
namespace App01_ConsultarCep
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCep;
		}

        private void BuscarCep(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCep(cep))
            {
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCep(cep);

                    if(end != null)
                    {
                        RESULTADO.Text = string.Format
                        ("Endereço: {0}-{1} {2} {3} - {4}",
                         end.localidade, end.uf, end.logradouro, end.complemento, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("Erro!", "O Endereço não foi localizado para o Cep" +
                            " Informado: " + cep, "OK");
                    }
                    
                }
                catch (Exception e)
                {
                    DisplayAlert("Erro Crítico!", e.Message, "OK");
                }
                
            }

        }

        private bool isValidCep(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("Erro!!", "CEP Inválido. O Cep deve conter 8 Caracteres!", "OK");
                valido = false;
            }

            int NovoCep = 0;
            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("Erro!!", "CEP Inválido. O Cep deve conter somente números!", "OK");
                valido = false;
            }

            return valido;
        }

    }
}
