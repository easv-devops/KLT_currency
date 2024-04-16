const {Selector} = require("testcafe");

fixture('Getting Started').page('http://144.91.64.53:8081/');

test('Afprøvning af testCafe', async  t =>
{
  //Constanter til dropdown 1
  const dropdown1 = Selector('#select1') //Her vælges dropdown element1
  const value1 =dropdown1.find('option') //Her vælges option under dropdown1

  //Constanter til dropdown 2
  const dropdown2 = Selector('#select2') //Her vælges dropdown element2
  const value2 =dropdown2.find('option') //Her vælges option under dropdown2


  const H1 = Selector('#h1-1') //Her vælges h1

    await t

    //input
      .typeText('#input1','100')

    //dropdown
      .click(dropdown1)
      .click(value1.withText('USD')) //Her vælges dropdown værdi value1
      .expect(dropdown1.value).eql('USD') //Her testes om vi fik den rigtige værdi

      .click(dropdown2)
      .click(value2.withText('EUR')) //Her vælges dropdown værdi value1
      .expect(dropdown2.value).eql('EUR') //Her testes om vi fik den rigtige værdi
      .wait(1000)


      //Click
      .click('#button1')
      .wait(2000)


      .expect(H1.textContent).eql('Convertion gives: 93') //Her testes om værdien i paragraf er Tekst1

});



//kør testen
//testcafe chrome TestCafe1.js --live
//Ctrl+S stopper testen
//Ctrl+r genstarter testen

