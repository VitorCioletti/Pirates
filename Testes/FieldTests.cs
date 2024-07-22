namespace Piratas.Servidor.Testes;

using System.Collections.Generic;
using Dominio;
using Dominio.Cartas.Duelo;
using Dominio.Cartas.DueloSurpresa;
using Dominio.Cartas.Embarcacao;
using Dominio.Cartas.Tesouro;
using Dominio.Cartas.Tripulacao;
using Dominio.Excecoes.Campo;
using NUnit.Framework;

public class FieldTests
{
    private Field _field;

    [SetUp]
    public void SetUp()
    {
        _field = new Field();
    }

    [Test]
    public void MustDamageShip()
    {
        var ironhull = new IronHull();

        int lifePreDamage = ironhull.Life;

        _field.Add(ironhull);

        _field.DamageShip();

        int lifePosDamage = _field.Ship.Life;

        Assert.Greater(lifePreDamage, lifePosDamage);
    }

    [Test]
    public void MustRemoveShipWhenSunk()
    {
        var ironHull = new IronHull();

        _field.Add(ironHull);

        int life = ironHull.Life;

        for (int i = 0; i <= life; i++)
        {
            _field.DamageShip();
        }

        Assert.AreEqual(null, _field.Ship);
    }

    [Test]
    public void MustAddShip()
    {
        var ironHull = new IronHull();

        _field.Add(ironHull);

        Assert.AreEqual(ironHull, _field.Ship);
    }

    [Test]
    public void DeveLevantarErroAoAdicionarEmbarcacaoEJaExistir()
    {
        var cascoAco = new IronHull();

        _field.Add(cascoAco);

        Assert.Throws<ShipAlreadyExistsException>(Adicionar);

        void Adicionar()
        {
            _field.Add(cascoAco);
        }
    }

    [Test]
    public void DeveTrocarEmbarcacao()
    {
        var cascoAco = new IronHull();
        var guerrilhaNaval = new NavalGuerrilla();

        _field.Add(cascoAco);

        _field.ChangeShip(guerrilhaNaval);

        Assert.AreEqual(guerrilhaNaval, _field.Ship);
    }

    [Test]
    public void DeveLevantarErroAoTrocarEmbarcacaoENaoExistirNenhuma()
    {
        var guerrilhaNaval = new NavalGuerrilla();

        Assert.Throws<NoShipException>(TrocarEmbarcacao);

        void TrocarEmbarcacao()
        {
            _field.ChangeShip(guerrilhaNaval);
        }
    }

    [Test]
    public void DeveAdicionarCanhao()
    {
        var canhao = new Cannon();

        _field.Add(canhao);

        Assert.AreEqual(canhao, _field.Cannons[0]);
    }

    [Test]
    public void DeveAdicionarCanhoes()
    {
        var canhoes = new List<Cannon> {new(), new()};

        _field.Add(canhoes);

        Assert.AreEqual(canhoes, _field.Cannons);
    }

    [Test]
    public void DeveRemoverCanhao()
    {
        var canhao = new Cannon();

        _field.Add(canhao);

        _field.Remover(canhao);

        Assert.AreEqual(0, _field.Cannons.Count);
    }

    [Test]
    public void DeveAdicionarTripulante()
    {
        var pirata = new Pirate();

        _field.Add(pirata);

        Assert.AreEqual(pirata, _field.Crew[0]);
    }

    [Test]
    public void DeveLevantarErroAoAdicionarETriuplacaoCheia()
    {
        for (int i = 0; i < Field.MaximumCrew; i++)
        {
            AdicionarPirata();
        }

        Assert.Throws<FullCrewException>(AdicionarPirata);

        void AdicionarPirata()
        {
            var pirata = new Pirate();

            _field.Add(pirata);
        }
    }

    [Test]
    public void DeveRemoverTripulante()
    {
        var pirata = new Pirate();

        _field.Add(pirata);
        _field.Remove(pirata);

        Assert.AreEqual(0, _field.Crew.Count);
    }

    [Test]
    public void DeveLevantarErroAoRemoverTripulanteENaoExistir()
    {
        Assert.Throws<EmptyCrewException>(Remover);

        void Remover()
        {
            var pirata = new Pirate();

            _field.Remove(pirata);
        }
    }

    [Test]
    public void DeveLevantarErroAoNaoEncontrarTripulante()
    {
        var pirata = new Pirate();

        _field.Add(pirata);
        Assert.Throws<CrewMemberNotFoundException>(Remover);

        void Remover()
        {
            var pirataARemover = new Pirate();

            _field.Remove(pirataARemover);
        }
    }

    [Test]
    public void DeveAfogarTripulacao()
    {
        var pirata = new Pirate();

        _field.Add(pirata);

        _field.DrownCrew();

        Assert.AreEqual(0, _field.Crew.Count);
    }

    [Test]
    public void DeveRemoverCartasDuelo()
    {
        var canhao = new Cannon();
        var ataqueSurpresa = new SurpriseAttack();

        _field.Add(canhao);
        _field.Add(ataqueSurpresa);

        _field.RemoveDuelCards();

        Assert.AreEqual(0, _field.Cannons.Count);
        Assert.AreEqual(0, _field.SurpriseDuel.Count);
    }

    [Test]
    public void DeveAdicionarCartaProtegida()
    {
        var tesouro = new Treasure(3);

        _field.AddProtected(tesouro);

        Assert.AreEqual(tesouro, _field.Protected[0]);
    }

    [Test]
    public void DeveRemoverProtegidasAoRemoverEmbarcacao()
    {
        var tesouro = new Treasure(3);
        var cascoAco = new IronHull();

        _field.Add(cascoAco);
        _field.AddProtected(tesouro);

        int vidaTotal = cascoAco.Life;

        for (int i = 0; i <= vidaTotal; i++)
        {
            _field.DamageShip();
        }

        Assert.AreEqual(null, _field.Ship);
    }

    [Test]
    public void DeveAdicionarDueloSurpresa()
    {
        var dueloSurpresa = new SurpriseAttack();

        _field.Add(dueloSurpresa);

        Assert.AreEqual(dueloSurpresa, _field.SurpriseDuel[0]);
    }
}
