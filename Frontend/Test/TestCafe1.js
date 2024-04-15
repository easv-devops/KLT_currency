const {Selector} = require("testcafe");

fixture('Getting Started').page('http://localhost:4200/');

test('Afprøvning af testCafe', async  t =>
{
  //Constanter til dropdown 1
  const dropdown1 = Selector('#select1') //Her vælges dropdown element1
  const value1 =dropdown1.find('option') //Her vælges option under dropdown

  //Constanter til dropdown 2
  const dropdown2 = Selector('#select2') //Her vælges dropdown element1
  const value2 =dropdown2.find('option') //Her vælges option under dropdown


  const H1 = Selector('#h1-1') //Her vælges p feltet med navnet p1

    await t
      //Click
      .click('#button1')
        .wait(2000)
    //input
      .typeText('#input1','Raghav')

    //dropdown
      .click(dropdown1)
      .click(value1.withText('EUR')) //Her vælges dropdown værdi value1
      .expect(dropdown.value).eql('EUR') //Her testes om vi fik den rigtige værdi

      .click(dropdown1)
      .click(value1.withText('USD')) //Her vælges dropdown værdi value1
      .expect(dropdown.value).eql('USD') //Her testes om vi fik den rigtige værdi




});



//kør testen
//testcafe chrome TestCafe1.js --live
//Ctrl+S stopper testen
//Ctrl+r genstarter testen

