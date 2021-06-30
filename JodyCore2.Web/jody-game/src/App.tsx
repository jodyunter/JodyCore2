import React from 'react';
import { TeamList, TeamListItem } from './TeamComponents';
import  getTeams from './TeamComponents';

const teams: Team[] = [
  {
    identifier: '25',
    name: 'Team 1',
    skill: 5
  },
  {
    identifier: '26',
    name: 'Team 2',
    skill: 5
  },
];


function App() {  
  return (
    getTeams().then(a => <TeamListItem teams={ a } />);
 
  );
}

export default App;