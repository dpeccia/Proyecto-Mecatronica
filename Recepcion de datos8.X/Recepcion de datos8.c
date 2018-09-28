#include <18F4550.h>
#include <stdlib.h>
#fuses HSPLL,NOWDT,NOPROTECT,NOLVP,NODEBUG,PLL5,CPUDIV1,VREGEN,MCLR,USBDIV,  // 48 MHz  para  el  USB y 48 MHz para  el resto del sistema
#use delay(clock=48000000)
#use rs232(baud=9600, xmit=pin_c6, rcv=pin_c7, bits=8, parity=N,stream=standard) 
#include <LCD420-FLEX.c>
#include <MATH.h>

#define sentidox PIN_D0
#define sentidoy PIN_D1
#define sentidoz PIN_D2
#define motorx PIN_D3
#define motory PIN_D4
#define motorz PIN_D5
#define cerox PIN_D6
#define ceroy PIN_D7
#define ceroz PIN_C0
#define profunz PIN_C1
#define dremel PIN_E0
#define enter PIN_E1
#define habilx PIN_E2
#define habily PIN_A0
#define habilz PIN_A1
#define canalb PIN_A2

char ch;
char mech[5];
char datox[7];
char datoy[7];
int i, j, h, flagfin, salir;
int contador;
signed int32 x, y, xini, yini, pasosx, pasosy;
float broca, pasosxflot, pasosyflot;
int flagx, flagy, flagxy, flagcomienzo, flagmecha, flagboton;
short int flagpaquete, flagajuste;
signed int32 pasosz, zini, z, alturamecha;

void moverz(void)
{
    output_low(habilz);
    pasosz = z - zini;

    while (pasosz > 0)
    {
        output_high(sentidoz); //para abajo
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
        pasosz = pasosz - 1;
    }
    while (pasosz < 0)
    {
        output_low(sentidoz); //para arriba
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
        pasosz = pasosz + 1;
    }
    zini = z;
}

void bajarz(void)
{
    while (pasosz > 0)
    { //rutina que baja el cabezal
        output_low(habilz);
        pasosz = pasosz - 1;
        output_high(sentidoz);
        output_toggle(motorz);
        delay_us(1200);
    }
}

void subirz(void)
{
    while (pasosz > 0)
    { //rutina que sube el cabezal
        output_low(sentidoz);
        pasosz = pasosz - 1;
        output_toggle(motorz);
        delay_us(1200);
    }
}

void conversionpasos(void)
{
    pasosxflot = (x - xini) / 200.0;
    pasosyflot = (y - yini) / 200.0;
    if (pasosxflot >= 0)
        pasosx = (signed long int) (pasosxflot + 0.5);
    else
        pasosx = (signed long int) (pasosxflot - 0.5);

    if (pasosyflot >= 0)
        pasosy = (signed long int) (pasosyflot + 0.5);
    else
        pasosy = (signed long int) (pasosyflot - 0.5);

    pasosx = pasosx * 2;
    pasosy = pasosy * 2;

}

void perforacion(void)
{
    output_high(dremel);
    while (flagpaquete == 0);
    while (flagpaquete == 1)
    {
        while (flagxy == 0);
        while (flagxy == 1)
        {
            x = (signed long long int) atof(datox);
            y = (signed long long int) atof(datoy);
            conversionpasos();
            xini = x;
            yini = y;
            clear_interrupt(INT_TIMER0);
            set_timer0(230);
            enable_interrupts(INT_TIMER0); //Habilita interrupción timer0
            flagx = 1;
            flagy = 1;
            while (flagxy == 1);
            z = alturamecha + 300;
            moverz();
            z = alturamecha; //trabajando a por paso completo son 1cm(100*0,2)/2
            moverz();
            putc('*');
        }
    }
    output_low(dremel);
}

void moverxy(void)
{
    clear_interrupt(INT_TIMER0);
    set_timer0(230);
    enable_interrupts(INT_TIMER0); //Habilita interrupción timer0
    flagx = 1;
    flagy = 1;
    flagxy = 1;
    while (flagxy == 1);
}

void posicionmecha(void)
{
    x = +150000;
    y = +200000;
    conversionpasos();
    xini = x;
    yini = y;
    // pasosx=+1500;     //pasos para ir a punto de prueba
    //pasosy=+1000;     //pasos para ir a puto de prueba
    contador = 2;
    moverxy();
    putc('M');
}

void profundidadmecha(void)
{
    lcd_putc("\fAjustando Z\n");
    lcd_putc("Profundidad de mecha");
    output_low(habilz);
    output_low(habilx);
    output_low(habily);
    output_low(motorz);
    while (input(profunz) == 1)
    {
        output_high(sentidoz);
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
        alturamecha = alturamecha + 1;
    }
    alturamecha = alturamecha - 200; //le resta el espesor de la base y le da un mm más
}

void ajusteceroz(void)
{
    output_low(habilz);
    lcd_putc("Buscando cero Z");
    while ((input(ceroz)) == 0)
    {
        output_low(sentidoz);
        output_toggle(motorz);
        delay_us(1200);
    }
    lcd_putc(" OK\n");
    zini = 0;
}

void ajustedecero(void)
{
    lcd_putc("\fBuscando origen\n");
    ajusteceroz();
    lcd_putc("Buscando cero X\n");
    lcd_putc("Buscando cero Y\n");
    output_low(habilx);
    output_low(habily);
    while ((input(cerox) == 0) || (input(ceroy)) == 0)
    {
        if (input(cerox) == 0)
        {
            output_low(sentidox);
            output_toggle(motorx);
        }
        if (input(cerox) == 1)
        {
            lcd_gotoxy(17, 3);
            lcd_putc("OK");
        }
        if (input(ceroy) == 0)
        {
            output_high(sentidoy);
            output_toggle(motory);
        }
        if (input(ceroy) == 1)
        {
            lcd_gotoxy(17, 4);
            lcd_putc("OK");
        }
        delay_us(1200);
    }
    lcd_gotoxy(17, 3);
    lcd_putc("OK");
    lcd_gotoxy(17, 4);
    lcd_putc("OK");
    output_low(motorx);
    output_low(motory);
    output_low(motorz);
    xini = 0;
    yini = 0;
}

void mecha(void)
{
    while (flagmecha == 0);
    while (flagmecha == 1);
    broca = atof(mech);
}


#INT_EXT2         //Atención a interrupción por cambio en RB2

ext_isr1()
{ //Función de interrupción
    putc('F');
    flagajuste = 0;
    salir = 1;
    disable_interrupts(INT_EXT2_L2H);
}

#INT_EXT         //Atención a interrupción por cambio en RB0

void INTEXT_isr(void)
{ //Función de interrupción
    output_low(motorz);
    if (input(canalb) == 0)
    {
        output_low(habilz);
        output_low(sentidoz); //mecha para abajo
        z = z - 2;
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
    }
    if (input(canalb) == 1)
    {
        output_low(habilz);
        output_high(sentidoz); //mecha para arriba
        z = z + 2;
        output_high(motorz);
        delay_us(1200);
        output_low(motorz);
        delay_us(1200);
    }
}
#int_TIMER0

void TIMER0_isr(void)
{
    contador = contador - 1;

    if (contador == 0)
    {
        contador = 2;
        if (pasosx == 0)
        {
            flagx = 0;
        }
        if (pasosx > 0)
        {
            output_high(sentidox);
            output_toggle(motorx);
            pasosx = pasosx - 1;
        }
        if (pasosx < 0)
        {
            output_low(sentidox);
            output_toggle(motorx);
            pasosx = pasosx + 1;
        }

        if (pasosy == 0)
        {
            flagy = 0;
        }
        if (pasosy > 0)
        {
            output_low(sentidoy);
            output_toggle(motory);
            pasosy = pasosy - 1;
        }
        if (pasosy < 0)
        {
            output_high(sentidoy);
            output_toggle(motory);
            pasosy = pasosy + 1;
        }

        if (flagx == 0 && flagy == 0)
        {
            flagxy = 0;
            disable_interrupts(INT_TIMER0); //deshabilita interrupción timer0
        }
    }
    set_timer0(230); //Se recarga el timer0
}

#int_rda

void serial_isr()
{
    ch = getchar();
    if (ch == 'F' && flagpaquete == 1)
    {
        flagpaquete = flagpaquete + 1;
        flagfin = 1;
    }
    if (ch == 'A')
    {
        flagajuste = flagajuste + 1;
    }
    if (ch == 'P')
    {
        flagpaquete = flagpaquete + 1;
    }
    if (ch == '*')
    {
        flagcomienzo = 1;
    }
    if (ch == 'M')
    {
        flagmecha = 1;
        h = 0;
    }
    if ((flagmecha) == 1 && (ch != 'M'))
    {

        mech[h] = ch;
        h = h + 1;
        if (h == 6)
        {
            flagmecha = 0;
        }
    }
    if (ch == 'X')
    {
        flagx = 1;
        i = 0;
    }
    if (ch == 'Y')
    {
        flagy = 1;
        j = 0;
    }
    if ((flagx) == 1 && (ch != 'X'))
    {

        datox[i] = ch;
        i = i + 1;
        if (i == 7)
        {
            flagx = 0;
            putc('*');
        }
    }
    if ((flagy) == 1 && (ch != 'Y'))
    {

        datoy[j] = ch;
        j = j + 1;
        if (j == 7)
        {
            flagy = 0;
            flagxy = 1;
            putc('*');
        }
    }
}
void main()
{
    output_high(habilx); //motor desenergizado
    output_high(habily);
    output_high(habilz);
    delay_ms(100);
    enable_interrupts(INT_RDA);
    setup_timer_0(RTCC_8_BIT | RTCC_DIV_256); //Configuración timer0

    enable_interrupts(INT_EXT2_L2H); //Habilita int. RB2?
    //ext_int_edge(L_TO_H);              //por flanco de subida
    enable_interrupts(GLOBAL); //Habilita interrupción general
    lcd_init();

    while (1)
    {
        flagfin = 0;
        flagmecha = 0;
        flagcomienzo = 0;
        xini = 0;
        yini = 0;
        zini = 0;
        flagajuste = 0;
        flagpaquete = 0;
        pasosx = 0;
        pasosz = 0;
        pasosy = 0;
        alturamecha = 0;
        x = 0;
        y = 0;
        z = 0;
        flagboton = 0;
        flagxy = 0;
        flagx = 0;
        flagy = 0;
        broca = 0;
        pasosxflot = 0;
        pasosyflot = 0;
        salir = 0;
        i = 0;
        j = 0;
        h = 0;
        lcd_putc("\fHAGA CLICK EN\n");
        lcd_putc("EN CALIBRAR EN EL\n");
        lcd_putc("PROGRAMA Y ESPERE \n");
        lcd_putc("UN MOMENTO.");
        while (flagcomienzo == 0);
        flagcomienzo = 0;
        ajustedecero();
        posicionmecha();
        mecha();
        printf(lcd_putc"\fPoner mecha %01.3fmm", broca);
        lcd_putc("\nPresione enter para\n");
        lcd_putc("comenzar el ajuste\n");
        lcd_putc("de la placa.");
        while (input(enter) == 0)
        {
        }
        delay_ms(10);
        while (input(enter) == 1)
        {
        }
        profundidadmecha();
        //ajustedecero();
        z = alturamecha; //******OK******
        moverz();
        putc('A');
        while (flagajuste == 0 && salir == 0);

        while (flagajuste == 1)
        {
            while (flagxy == 0);
            while (flagxy == 1)
            {
                x = (signed long long int) atof(datox);
                y = (signed long long int) atof(datoy);
                conversionpasos();
                xini = x;
                yini = y;
                clear_interrupt(INT_TIMER0);
                set_timer0(230);
                enable_interrupts(INT_TIMER0); //Habilita interrupción timer0
                flagx = 1;
                flagy = 1;
                while (flagxy == 1);
                //bajarz();
                //moverz();
                lcd_putc("\fGire para subir o\n");
                lcd_putc("bajar mecha.Presione\n");
                lcd_putc("\enter nuevo punto.\n");
                lcd_putc("Boton rojo salir.\n");
                enable_interrupts(INT_EXT2_L2H);
                while (input(enter) == 0 && salir == 0)
                {
                }
                delay_ms(10);
                while (input(enter) == 1 && salir == 0)
                {
                }
                if (input(enter) == 0 && salir == 0)
                {
                    zini = z;
                    z = alturamecha;
                    moverz();
                    putc('A');
                }
            }
        }
        disable_interrupts(INT_EXT2_L2H);
        //salir=0;
        while (flagfin == 0)
        {
            ajustedecero();
            if (salir == 0)
            {
                posicionmecha();
                mecha();
                lcd_putc("\fPor favor coloque\n");
                printf(lcd_putc"mecha de %01.2fmm y", broca);
                lcd_putc("\npresione enter");

                while (input(enter) == 0)
                {
                }
                delay_ms(10);
                while (input(enter) == 1)
                {
                }
                profundidadmecha();
                ajustedecero();
                puts("OK");
            }
            contador = 2;
            lcd_putc("\fPerforadora Lista\n");
            lcd_putc("para operar pulse *\n");
            lcd_putc("para comenzar");

            puts("OK");
            while (flagcomienzo == 0);
            lcd_putc("\fPERFORANDO\n");
            flagcomienzo = 0;
            perforacion();
            salir = 0;
        }
    }
}