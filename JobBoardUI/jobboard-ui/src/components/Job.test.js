import React from 'react';
import ReactDOM from 'react-dom';
import Job from './Job';

it('renders without crashing', () => {
  const div = document.createElement('div');
  ReactDOM.render(<Job />, div);
  ReactDOM.unmountComponentAtNode(div);
});
