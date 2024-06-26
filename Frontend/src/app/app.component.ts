import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {currencyModel, ResponseDto} from "./models";
import {environment} from "../environments/environment";
import {firstValueFrom} from "rxjs";


// @ts-ignore
@Component({
  selector: 'app-root',
  template: `



    <ion-content>
    <ion-header>Currency converter </ion-header>




            <ion-row>
              <ion-col>

          <select id="select1" placeholder="Source" [(ngModel)]="source" >


              <option value="USD">USD</option>
              <option value="EUR">EUR</option>
              <option value="GBP">GBP</option>
              <option value="JPY">JPY</option>
              <option value="AUD">AUD</option>


          </select>
              </ion-col>

              <ion-col>
            <select id="select2" placeholder="Source" [(ngModel)]="target" >

                <option value="USD">USD</option>
                <option value="EUR">EUR</option>
                <option value="GBP">GBP</option>
                <option value="JPY">JPY</option>
                <option value="AUD">AUD</option>


              </select>

              </ion-col>

            </ion-row>



      <input id="input1" [(ngModel)]="value1">

        <button id="button1" (click)="convert()">Convert</button>





        <h1 id="h1-1">Convertion gives: {{convertion}}</h1>
      

    <ion-row>

    <h1>History</h1>

    </ion-row>
      <ion-row>
      <ion-col style=" border: 2px solid #000;">
        <h1>Date</h1>
      </ion-col>

      <ion-col style=" border: 2px solid #000;">
        <h1>Source</h1>
      </ion-col>

      <ion-col style=" border: 2px solid #000;">
        <h1>Target</h1>
      </ion-col>

      <ion-col style=" border: 2px solid #000;">
        <h1>Value</h1>
      </ion-col>

      <ion-col style=" border: 2px solid #000;">
        <h1>Result</h1>
      </ion-col>



    </ion-row>

    <div *ngFor="let his of history">


      <ion-row >
        <ion-col style=" border: 2px solid #000;">
          <h1>{{his.date}}</h1>
        </ion-col>

        <ion-col style=" border: 2px solid #000;">
          <h1>{{his.source}}</h1>
        </ion-col>

        <ion-col style=" border: 2px solid #000;">
          <h1>{{his.target}}</h1>
        </ion-col>

        <ion-col style=" border: 2px solid #000;">
          <h1>{{his.value}}</h1>
        </ion-col>

        <ion-col style=" border: 2px solid #000;">
          <h1>{{his.result}}</h1>
        </ion-col>

      </ion-row>

    </div>


    </ion-content>






  `,

})
export class AppComponent implements OnInit{

  source: string="";
  target: string="";
  currencies: string[]=["USD","EUR","GBP","JPY","AUD"];
  value1: number=0;
  convertion: number=0;
  history: currencyModel[]=[];



  constructor(private readonly http: HttpClient) {}

  async convert() {



    let currencyModel: currencyModel = {
      date: new Date(),
      source: this.source,
      target: this.target,
      value: this.value1,
      result: 0

    }

    var req = this.http.post<ResponseDto<currencyModel>>(environment.baseUrl+'/currency/post',
        currencyModel);
    var response =await firstValueFrom<ResponseDto<currencyModel>>(req);


    currencyModel=response.responseData;
    this.convertion=currencyModel.result;

    this.getHistory();


  }

  ngOnInit(): void {

this.getHistory();


  }


  async getHistory()
  {
    var result= await firstValueFrom(this.http.get<ResponseDto<currencyModel[]>>(environment.baseUrl+ "/currency/get"))
    this.history=result.responseData;


  }



}
