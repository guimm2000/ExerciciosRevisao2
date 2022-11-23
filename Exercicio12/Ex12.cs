using System;

public class Carro
{
    private readonly string placa;
    public string Placa
    {
        get
        {
            return placa;
        }
        init
        {
            placa = value;
        }
    }

    private readonly string modelo;
    public string Modelo
    {
        get
        {
            return modelo;
        } 
        init
        {
            modelo = value;
        }
    }

    public Motor Motor { get; private set; }

    public Carro(string placa, string modelo, Motor motor)
    {
        this.Placa = placa;
        this.Modelo = modelo;
        this.Motor = motor;
        motor.InstalarMotor(this);
    }

    public void TrocarMotor(Motor novoMotor)
    {
        Motor antigoMotor = this.Motor;
        this.Motor = novoMotor;
        antigoMotor.RemoverMotor();
    }

    public int VelocidadeMaxima()
    {
        switch (this.Motor.Cilindrada)
        {
            case <= 1.0f:
                return 140;

            case <= 1.6f:
                return 160;

            case <= 2.0f:
                return 180;

            case >= 2.0f:
                return 220;

            default: 
                return 0;
                break;
        }
    }

}

public class Motor
{
    private readonly float cilindrada;
    public float Cilindrada
    {
        get
        {
            return cilindrada;
        }
        init
        {
            cilindrada = value;
        }
    }

    public Carro Carro { get; private set; }

    public Motor(float cilindrada)
    {
        this.Cilindrada = cilindrada;
    }

    public void InstalarMotor(Carro carro)
    {
        if (Carro.Equals(null))
        {
            this.Carro = carro;
            return;
        }
        throw new MotorExcecao("Motor ja instalado em um carro!");
    }

    public void RemoverMotor()
    {
        if (this.Carro.Motor.Equals(null))
        {
            this.Carro = null;
            return;
        }
        Console.WriteLine("Nao e possivel remover o motor deste carro, pois ficaria sem motor!");
    }
}

public class MotorExcecao : Exception
{
    public MotorExcecao(string message)
        : base(message)
    { 
    }
}