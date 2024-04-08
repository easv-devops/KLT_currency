import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {currencyModel, ResponseDto} from "./models";
import {environment} from "../environments/environment";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-root',
  template: `

  <ion-content>

    <ion-header>Currency converter </ion-header>


    <ion-row>


        <ion-item>
          <ion-select placeholder="Source" [(ngModel)]="source" >
            <div slot="label">
              <ion-text >Name</ion-text>
            </div>

            <ion-select-option *ngFor="let source of currencies" [value]="source">{{ source }}</ion-select-option>
          </ion-select>
        </ion-item>



      <ion-item>
        <ion-select placeholder="Source" [(ngModel)]="target" >
          <div slot="label">
            <ion-text >Name</ion-text>
          </div>
          <ion-select-option *ngFor="let target of currencies" [value]="target">{{ target }}</ion-select-option>
        </ion-select>

      </ion-item>


    </ion-row>
    <ion-row>

      <ion-item>
      <ion-input [(ngModel)]="value1">Amount</ion-input>
      </ion-item>
        <ion-button (click)="convert()">Convert</ion-button>

      <br><br><br><br><br>
    </ion-row>
    <ion-row>

        <h1>Convertion gives: {{convertion}}</h1>


    </ion-row>



  </ion-content>





  `,

})
export class AppComponent {

  source: string="";
  target: string="";
  currencies: string[]=["USD","EUR","GBP","JPY","AUD"];
  value1: number=0;
  convertion: number=0;

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



  }
}
