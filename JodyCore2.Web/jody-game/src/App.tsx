import React, { useState } from 'react';
import { TeamList } from './TeamComponents';

function App() {  
  const [teams, setTeams] = useState([]);

  function fetchTeamshandler() {
    fetch('https://localhost:5000/api/Team/all').then(response =>
    {
      return response.json();
    }).then((data) => {
      setTeams(data);
    });

  }
 
  return (
    <React.Fragment>
        <section>
          <button onClick={fetchTeamshandler}>Get Teams</button>
        </section>
        <TeamList teams={ teams } />
    </React.Fragment>
    
  );
  
}

export default App;