import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { Provider } from "react-redux"
import thunk from "redux-thunk"
import { createStore, applyMiddleware, Store } from "redux"

import 'bootstrap/dist/css/bootstrap.min.css';
import { teamReducer } from './store/reducer';

const store: Store<TeamState, TeamAction> & {
  dispatch: DispatchType
} = createStore(teamReducer, applyMiddleware(thunk))

//import './index.css'
const rootElement = document.getElementById('root')

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  rootElement

);